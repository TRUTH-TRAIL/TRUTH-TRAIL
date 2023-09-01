using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMoveState : AIState
{
    List<Transform> Locations = new List<Transform>();
    NavMeshPathManager pathManager;
    float maxSpeed = 1f;
    float moveSpeed = 1f;

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
        agent._navMeshAgent.speed = agent.SetSpeed(moveSpeed * maxSpeed);
        pathManager.SetShortestDestination(agent._navMeshAgent);
        Debug.Log($"{pathManager.ShowNodes()[pathManager.curDestination].name} is {pathManager.ShowNodeVisit()[pathManager.curDestination]}");
        Debug.Log("Move Start!");
    }


    public void Update(AIAgent agent)
    {
        if (agent._sensor.Objects.Count > 0 || agent.AggroGauge >= 90)
        {
            agent._stateMachine.ChangeState(AIStateId.ChasePlayer);
        }

        if ((agent._navMeshAgent.destination - agent.transform.position).sqrMagnitude <= agent._navMeshAgent.stoppingDistance * agent._navMeshAgent.stoppingDistance)
        {
            pathManager.SetVisit(true);
            pathManager.SetShortestDestination(agent._navMeshAgent);
        }

    }

    public void Exit(AIAgent agent)
    {
        agent._animator.Play("Idle And Walk");
    }



}
