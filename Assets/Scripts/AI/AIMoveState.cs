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
        if (agent._sensor.IsInSight(agent._playerTransform.gameObject) || GameManager.Instance.aggroGauge >= 100)
        {
            agent._stateMachine.ChangeState(AIStateId.ChasePlayer);
        }

        if((agent._navMeshAgent.destination - agent.transform.position).sqrMagnitude <= agent._navMeshAgent.stoppingDistance * agent._navMeshAgent.stoppingDistance)
        {
            pathManager.SetVisit(true);
            pathManager.SetShortestDestination(agent._navMeshAgent);
        }


    }

    public void Exit(AIAgent agent)
    {
        
    }



}
