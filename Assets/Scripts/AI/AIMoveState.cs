using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoveState : AIState
{
    List<Transform> Locations = new List<Transform>();
    NavMeshPathManager pathManager;
    float animeTime = 3.0f;

    public AIMoveState()
    {
        pathManager = new NavMeshPathManager();
    }
    public AIStateId GetId()
    {
        return AIStateId.Move;
    }

    public void Enter(AIAgent agent)
    {
        agent._navMeshAgent.speed = 1f;
        pathManager.SetShortestDestination(agent._navMeshAgent);
        Debug.Log($"{pathManager.ShowNodes()[pathManager.curDestination].name} is {pathManager.ShowNodeVisit()[pathManager.curDestination]}");
        Debug.Log("Move Start!");
    }


    public void Update(AIAgent agent)
    {
        if (agent._sensor.Objects.Count > 0 || GameManager.Instance.aggroGauge >= 100)
        {
            agent._stateMachine.ChangeState(AIStateId.ChasePlayer);
        }

        if((agent._navMeshAgent.destination - agent.transform.position).sqrMagnitude <= agent._navMeshAgent.stoppingDistance * agent._navMeshAgent.stoppingDistance)
        {
            if (!agent._animator.GetCurrentAnimatorStateInfo(0).IsName("Find"))
            {
                agent._navMeshAgent.isStopped = true;
                agent._navMeshAgent.velocity = Vector3.zero;
                agent._animator.Play("Find");
                return;
            }
        }

        if (agent._animator.GetCurrentAnimatorStateInfo(0).IsName("Find"))
        {
            if (agent._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Debug.Log("Move Start!");
                pathManager.SetVisit(true);
                pathManager.SetShortestDestination(agent._navMeshAgent);
                agent._animator.Play("Idle And Walk", -1, 0);
                agent._navMeshAgent.isStopped = false;
            }
        }

    }

    public void Exit(AIAgent agent)
    {
        agent._animator.Play("Idle And Walk");
    }



}
