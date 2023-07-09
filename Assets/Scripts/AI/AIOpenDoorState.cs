using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpenDoorState : AIState
{
    public AIStateId GetId()
    {
        return AIStateId.OpenDoor;
    }


    public void Enter(AIAgent agent)
    {
        /**
         * Start OpenDoorAnimation
         * OpenDoor
         * agent._stateMachine.ChangeState(AIStateId.Idle) // 모든 상태 변환은 idle에서 판단한다.
         */
    }

    public void Update(AIAgent agent)
    {
        throw new System.NotImplementedException();
    }

    public void Exit(AIAgent agent)
    {
        throw new System.NotImplementedException();
    }
}
