using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AlleyNav : MonoBehaviour
{
    //목적지
    public Transform target;
    //요원
    NavMeshAgent agent;
    public Animator anim;
    enum State
    {
        Idle,
        Walk,
        Attack,
        Find
    }

    State state;

    // Start is called before the first frame update
    void Start()
    {
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
        agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 10)
        {
            state = State.Walk;
            anim.SetTrigger("Walk");
        }
    }

    private void UpdateWalk()
    {
        //남은 거리가 2미터라면 공격한다.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2)
        {
            state = State.Attack;
            anim.SetTrigger("Hit");
        }
        //타겟 방향으로 이동하다가
        agent.speed = 3.5f;
        //요원에게 목적지를 알려준다.
        agent.destination = target.transform.position;
        StartCoroutine(ReFind());
    }

    private void UpdateIdle()
    {
        agent.speed = 0;
        //생성될때 목적지(Player)를 찿는다.
        target = GameObject.Find("Player").transform;
        //target을 찾으면 Run상태로 전이하고 싶다.
        if (target != null)
        {
            state = State.Walk;
            //이렇게 state값을 바꿨다고 animation까지 바뀔까? no! 동기화를 해줘야한다.
            anim.SetTrigger("Walk");
        }
    }

    IEnumerator ReFind(){
        yield return new WaitForSeconds(5.0f);
        state = State.Attack;
        //타겟 방향으로 이동하다가
        //anim.SetTrigger("Idle");
    }

    IEnumerator ReIdle()
    {
        while(true){
            target = GameObject.Find("Player").transform;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if(distance > 30)
            yield return new WaitForSeconds(3.0f);
        }
    }
}
