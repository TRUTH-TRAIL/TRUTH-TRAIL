using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AlleyNav : MonoBehaviour
{
    //������
    public Transform target;
    //���
    NavMeshAgent agent;
    public Animator anim;
    enum State
    {
        Idle,
        Run,
        Attack
    }

    State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        //����� �������༭
        agent = GetComponent<NavMeshAgent>();

        //�����ɶ� ������(Player)�� �O�´�.
        //target = GameObject.Find("Player").transform;
        //������� �������� �˷��ش�.
        //agent.destination = target.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //���� state�� idle�̶��
        if (state == State.Idle)
        {
            UpdateIdle();
        }
        else if (state == State.Run)
        {
            UpdateRun();
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
        if (distance > 2)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
    }

    private void UpdateRun()
    {
        //���� �Ÿ��� 2���Ͷ�� �����Ѵ�.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2)
        {
            state = State.Attack;
            anim.SetTrigger("Hit");
        }

        //Ÿ�� �������� �̵��ϴٰ�
        agent.speed = 3.5f;
        //������� �������� �˷��ش�.
        agent.destination = target.transform.position;
    }

    private void UpdateIdle()
    {
        agent.speed = 0;
        //�����ɶ� ������(Player)�� �O�´�.
        target = GameObject.Find("Player").transform;
        //target�� ã���� Run���·� �����ϰ� �ʹ�.
        if (target != null)
        {
            state = State.Run;
            //�̷��� state���� �ٲ�ٰ� animation���� �ٲ��? no! ����ȭ�� ������Ѵ�.
            anim.SetTrigger("Run");
        }
    }
}
