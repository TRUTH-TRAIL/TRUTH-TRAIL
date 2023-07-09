using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFindState : AIState
{
    float _time = 0.0f;
    float _findTime = 1.0f;
    public AIStateId GetId()
    {
        return AIStateId.FindPlayer;
    }


    public void Enter(AIAgent agent)
    {
        throw new System.NotImplementedException();
    }

    public void Update(AIAgent agent)
    {
        if (GameManager.Instance.aggroGauge <= 0)
            agent._stateMachine.ChangeState(AIStateId.Idle);
        GameManager.Instance.aggroGauge -= 1;


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
        throw new System.NotImplementedException();
    }
}
