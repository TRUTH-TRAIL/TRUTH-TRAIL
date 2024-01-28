using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AlleyNav : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    private Curses curses;
    private enum State
    {
        Idle,
        Walk,
        Attack
    }
    private State state;
    private string[] str;
    private int i;
    private int p;
    private int spotn;
    private bool Attack_state;
    //float timeSpan;
    bool curseOn;
    [SerializeField] GameObject BGM;
    [SerializeField] bool DebugMode = false;
    [SerializeField] bool PlayerView = false;
    [Range(0f, 360f)] [SerializeField] float ViewAngle = 0f;
    [SerializeField] float ViewRadius = 1f;
    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask ObstacleMask;
    //bool ending = false;
    // Start is called before the first frame update
    void Start()
    {
        curseOn = false;
       // timeSpan = 0;
        Attack_state = true;
        p = 0;
        i = 0;
        spotn = Random.Range(4, 8);
        state = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        curses = GameObject.Find("CurseManager").GetComponent<Curses>();
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.name == "Alley_open"){
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
        else{
            if (state == State.Idle)
            {
                UpdateIdle();
            }
            else if (state == State.Walk)
            {
                UpdateWalk();
            }
            else if (state == State.Attack && Attack_state)
            {
                UpdateAttack();
            }
        }
        //Debug.Log(state);
        //만약 state�????? idle?��?���?????
    }
    void OnDrawGizmos() {
        if (!DebugMode) return;
        Vector3 myPos = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, ViewRadius);
        float lookingAngle = transform.eulerAngles.y;  //캐릭?���??? 바라보는 방향?�� 각도
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + ViewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - ViewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);

        Collider[] Targets = Physics.OverlapSphere(myPos, ViewRadius, TargetMask);

        if (Targets.Length == 0) return;
        foreach(Collider EnemyColli in Targets)
        {
            Vector3 targetPos = EnemyColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if(targetAngle <= ViewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
            {
                PlayerView = true;
                if (DebugMode) Debug.DrawLine(myPos, targetPos, Color.red);
            }
        }
    }

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    private void UpdateAttack()
    {
      //  Debug.Log(Attack_state);
        if(!GameObject.Find("CurseManager").GetComponent<Curses>().activeCurse && curseOn){
            state = State.Idle;
            curseOn = false;
        }
        agent.destination = target.position;
        agent.speed = target.GetComponent<PlayerController>().walkSpeed + 1;
        if(Vector3.Distance(transform.position, target.position) < 3.0f){
            Attack_state = false;
            target.GetChild(1).transform.GetChild(1).GetComponent<blinkSpot>().C_Blink();
            target.GetChild(1).transform.GetChild(1).GetComponent<blinkSpot>().Alley_pos = this.transform.position;
            this.transform.GetComponent<NavMeshAgent>().enabled = false;
            anim.SetTrigger("Idle");
            BGM.SetActive(false);
            if(curses.die==false){
                curses.die = true;
            }
        }
        if((Vector3.Distance(transform.position, target.position) > 12.0f)){ // && ???�??? 발생 모드�??? ?��?�� ?��
            Attack_state = false;
        }
       /* timeSpan += Time.deltaTime;
        if(timeSpan >= 10.0f){
            timeSpan = 0;
            i = 0;
            state = State.Idle;
            Attack_state = false;
            StartCoroutine(AttackChange());
        }*/
    }

    private void UpdateWalk()
    {
        anim.SetTrigger("Walk");
        agent.speed = 1f;
        SMove(str);
        if((Vector3.Distance(transform.position, target.position) <= 12.0f) ||
            ((Vector3.Distance(transform.position, target.position) <= 24.0f) && PlayerView == true)){ // ???�??? 발동{ //&& i != 0){
            state = State.Attack;
            Attack_state = true;
        }
        if(GameObject.Find("CurseManager").GetComponent<Curses>().activeCurse){
            curseOn = true;
            state = State.Attack;
            Attack_state = true;
            agent.speed = 3.5f * 3.0f;
        }
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
            //SpotNum(spotn);
            SpotNum(0);
        }
    }
    public void SpotNum(int s)
    {
        Debug.Log(s);
        switch(s){
            case 0:
                p = Random.Range(0, 2);
                break;
            case 1:
                p = Random.Range(0, 3);
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
                        str = new string[1]{"6_spot"};
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
                    // 10초간 �?????만히x
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
