using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFindState : AIState
{
    float _time = 0.0f;
    float _findTime = 1.0f;
    float _speed = 2f;
    public AIStateId GetId()
    {
        return AIStateId.FindPlayer;
    }


    public void Enter(AIAgent agent)
    {
        agent._navMeshAgent.speed = agent.SetSpeed(_speed);
    }

    public void Update(AIAgent agent)
    {
        if (agent._sensor.Objects.Count > 0 || agent.AggroGauge >= 90)
        {
            agent._stateMachine.ChangeState(AIStateId.ChasePlayer);
        }
        if (agent.AggroGauge <= 0)
            agent._stateMachine.ChangeState(AIStateId.Idle);


        /**
         * AggroGauge is Down
         * 
         * 
         * if(AggroGauge <= 0)
         *  agent._stateMachine.ChangeState(AIStateId.Idle); 
         */
    }

    public void Exit(AIAgent agent)
    {

    }
}
