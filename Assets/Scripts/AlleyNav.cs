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
    public enum State
    {
        Idle,
        Walk,
        Attack
    }
    public State state;
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
    AlleySpot alleySpot;
    // Start is called before the first frame update
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
    void Start()
    {
        alleySpot = GameObject.Find("spot").transform.GetComponent<AlleySpot>();
        curseOn = false;
       // timeSpan = 0;
        Attack_state = true;
        p = 0;
        spotn = Random.Range(4, 8);
        state = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        curses = GameObject.Find("CurseManager").GetComponent<Curses>();
    }
    // Update is called once per frame
    void Update()
    {
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
    private void UpdateIdle()
    {
        if(alleySpot.i != 0 && agent.speed != 0){
            state = State.Walk;
        }
        else if(agent.speed == 0){
            state = State.Idle;
            anim.SetTrigger("Idle");
        }
        else if(alleySpot.i == 0){
            alleySpot.SpotNum(spotn);
        }
    }
    private void UpdateWalk()
    {
        anim.SetTrigger("Walk");
        agent.speed = 5f;
        alleySpot.SMove(alleySpot.str);
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
    private void UpdateAttack()
    {
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
}
