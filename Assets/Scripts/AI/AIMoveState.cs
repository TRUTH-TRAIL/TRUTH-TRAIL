using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoveState : AIState
{
    List<Transform> Locations = new List<Transform>();
    public AIStateId GetId()
    {
        return AIStateId.Move;
    }

    public void Init()
    {
        Transform[] locs = GameObject.FindGameObjectWithTag("Location").transform.GetComponentsInChildren<Transform>();
        foreach(Transform loc in locs)
        {
            Locations.Add(loc);
        }
    }
    public void Enter(AIAgent agent)
    {
        agent._navMeshAgent.speed = 1f;
        if (Locations.Count <= 0)
        {
            Init();
        }
        int rand = Random.Range(0, Locations.Count);
        agent._navMeshAgent.SetDestination(Locations[rand].transform.position);
    }


    public void Update(AIAgent agent)
    {
        if (agent._sensor.IsInSight(agent._playerTransform.gameObject) || GameManager.Instance.aggroGauge >= 100)
        {
            agent._stateMachine.ChangeState(AIStateId.ChasePlayer);
        }

        
    }

    public void Exit(AIAgent agent)
    {
        
    }

    public void SetDestination()
    {

    }


}
