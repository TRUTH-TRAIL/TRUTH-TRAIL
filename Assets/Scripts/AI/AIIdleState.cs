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
        if (agent._sensor.Objects.Count > 0 || agent.AggroGauge >= 90)
        {
            agent._stateMachine.ChangeState(AIStateId.ChasePlayer);
        }
        //Find
        else if (agent.AggroGauge > 20)
        {
            agent._stateMachine.ChangeState(AIStateId.FindPlayer);
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
