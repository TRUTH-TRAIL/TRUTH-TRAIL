using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AlleyNav : MonoBehaviour
{
    //요원
    public NavMeshAgent agent;
    public Animator anim;
    enum State
    {
        Idle,
        Walk,
        Attack
    }
    State state;
    string[] str;
    int i;
    int spotn;
    bool stand;
    int p;
    // Start is called before the first frame update
    void Start()
    {
        stand = false;
        p = 0;
        i = 0;
        spotn = Random.Range(0, 7);
        state = State.Idle;
        //요원을 정의해줘서
        agent = GetComponent<NavMeshAgent>();

        //생성될때 목적지(Player)를 찿는다.
        //target = GameObject.Find("Player").transform;
        //요원에게 목적지를 알려준다.
        //agent.destination = target.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        //만약 state가 idle이라면
        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Walk)
        {
            UpdateWalk();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
    }

    private void UpdateAttack()
    {
      /*  agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 10)
        {
            state = State.Walk;
            anim.SetTrigger("Walk");
        }*/
    }

    private void UpdateWalk()
    {
        anim.SetTrigger("Walk");
        agent.speed = 3.5f;
        SMove(str);
      /*  //남은 거리가 2미터라면 공격한다.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2)
        {
            state = State.Attack;
            anim.SetTrigger("Hit");
        }
        //타겟 방향으로 이동하다가
        agent.speed = 3.5f;
        //요원에게 목적지를 알려준다.
      //  agent.destination = target.transform.position;*/
    }

    private void UpdateIdle()
    {
        if(i != 0 && agent.speed != 0){
            state = State.Walk;
        }
        else if(agent.speed == 0){
            state = State.Idle;
            anim.SetTrigger("Idle");
        }
        else if(i == 0){
            SpotNum(spotn);
        }
        //생성될때 목적지(Player)를 찿는다.
       //int r = AlleySpot.Instance.SpotNum(s);
        //target을 찾으면 Run상태로 전이하고 싶다.
      //  if (target != null)
      //  {
           // state = State.Walk;
            //이렇게 state값을 바꿨다고 animation까지 바뀔까? no! 동기화를 해줘야한다.
      //  }
    }
    public void SpotNum(int s)
    {
        Debug.Log(s);
        switch(s){
            case 0:
                p = Random.Range(0, 2);
                break;
            case 1:
                p = Random.Range(0, 2);
                break;
            case 2:
                p = Random.Range(0, 1);
                break;
            case 3:
                p = Random.Range(0, 4);
                break;
            case 4:
                p = Random.Range(0, 1);
                break;
            case 5:
                p = Random.Range(0, 1);
                break;
            case 6:
                p = 0;
                break;
            case 7:
                p = Random.Range(0, 1);
                break;
            case 8:
                p = Random.Range(0, 2);
                break;
            default:
                break;
        }
        spotn = SpotMove(s, p);
    }
    public int SpotMove(int s, int p){
        int spotNumber = 0;
        switch(s){
            case 0:
                switch(p){
                    case 0:
                        str = new string[3]{"0_spot_1", "8_spot", "4_spot"};
                        spotNumber = 4;
                        break;
                    case 1:
                        str = new string[1]{"0_spot"};
                        spotNumber = 0;
                        break;
                }
                break;
            case 1:
                switch(p){
                    case 0:
                        str = new string[1]{"6_spot_1"};
                        spotNumber = 6;
                        break;
                    case 1:
                        str = new string[1]{"3_spot"};
                        spotNumber = 3;
                        break;
                    case 2:
                        str = new string[1]{"0_spot"};
                        spotNumber = 0;
                        break;
                }
                break;
            case 2:
                switch(p){
                    case 0:
                        str = new string[1]{"1_spot"};
                        spotNumber = 1;
                        break;
                    case 1:
                        str = new string[2]{"8_spot", "4_spot"};
                        spotNumber = 4;
                        break;
                }
                break;
            case 3:
                switch(p){
                    case 0:
                        str = new string[2]{"3_spot_1", "8_spot"};
                        spotNumber = 8;
                        break;
                    case 1:
                        str = new string[2]{"3_spot_2", "8_spot"};
                        spotNumber = 8;
                        break;
                    case 2:
                        str = new string[1]{"2_spot"};
                        spotNumber = 2;
                        break;
                    case 3:
                        str = new string[1]{"3_spot_3"};
                        spotNumber = 3;
                        break;
                }
                break;
            case 4:
                switch(p){
                    case 0:
                        str = new string[4]{"7_spot", "4_spot_1", "4_spot_2", "7_spot"};
                        spotNumber = 7;
                        break;
                    case 1:
                        str = new string[3]{"3_spot_2", "4_spot_3", "6_spot"};
                        spotNumber = 6;
                        break;
                }
                break;
            case 5:
                switch(p){
                    case 0:
                        str = new string[7]{"5_spot_1", "5_spot_2", "6_spot", "5_spot_3", "4_spot_3", "5_spot_4", "6_spot"};
                        spotNumber = 6;
                        break;
                    case 1:
                        str = new string[4]{"5_spot", "5_spot_4", "5_spot", "1_spot"};
                        spotNumber = 1;
                        break;
                    }
                break;
            case 6:
                switch(p){
                    case 0:
                        str = new string[2]{"5_spot", "1_spot"};
                        spotNumber = 1;
                        break;
                   /* case 1:
                        str = new string[3]{"5_spot_3", "4_spot_3", "7_spot"};
                        spotNumber = 7;
                        break;
                    case 2:
                        str = new string[1]{"4_spot"};
                        spotNumber = 4;
                        break;*/
                }
                break;
            case 7:
                switch(p){
                    case 0:
                        str = new string[7]{"4_spot_2", "4_spot_1", "6_spot", "5_spot_3", "4_spot_3", "5_spot_4", "6_spot"};
                        spotNumber = 6;
                        break;
                    case 1:
                    // 10초간 가만히x
                        str = new string[3]{"7_spot_1", "5_spot_3", "6_spot"};
                        spotNumber = 6;
                        break;
                }
                break;
            case 8:
                switch(p){
                    case 0:
                        str = new string[1]{"4_spot"};
                        spotNumber = 4;
                        break;
                    case 1:
                        str = new string[1]{"1_spot"};
                        spotNumber = 1;
                        break;
                    case 2:
                        str = new string[1]{"2_spot"};
                        spotNumber = 2;
                        break;
                }
                break;
            default:
                break;
        }
        state = State.Walk;
        return spotNumber;
    }
    public void SMove(string[] s){
        Debug.Log(i);
        if(s.Length == i){
            i = 0;
            SpotNum(spotn);
        }
        agent.SetDestination(GameObject.Find(s[i]).transform.position);
        if(!agent.pathPending){
            if(agent.remainingDistance <= agent.stoppingDistance){
                if(!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if(s[i] == "3_spot_3"){
                        StartCoroutine(Standstill());
                    }
                    else if(s[i] == "5_spot_4"){
                        if(p == 1){
                            StartCoroutine(Standstill());
                        }
                    }
                    else if(s[i] == "0_spot"){
                        if(p == 1){
                            StartCoroutine(Standstill());
                        }
                    }
                    i++;
                    state = State.Idle;
                } 
            }
        }
    }
    IEnumerator Standstill(){
        agent.speed = 0;
        state = State.Idle;
        yield return new WaitForSeconds(7.0f);
        state = State.Walk;
    }
}
