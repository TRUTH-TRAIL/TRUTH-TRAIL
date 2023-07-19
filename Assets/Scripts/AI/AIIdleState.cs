using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{

    public AIStateId GetId()
    {
        return AIStateId.Idle;
    }

    public void Enter(AIAgent agent)
    {
        //Chase
        if (agent._sensor.IsInSight(agent._playerTransform.gameObject) || GameManager.Instance.aggroGauge >= 100)
        {
            agent._stateMachine.ChangeState(AIStateId.ChasePlayer);
        }
        //Find
        else if (GameManager.Instance.aggroGauge > 0)
        {

        }
        //Move
        else
        {
            agent._stateMachine.ChangeState(AIStateId.Move);
        }
    }

    public void Exit(AIAgent agent)
    {

    }



    public void Update(AIAgent agent)
    {

    }
}
