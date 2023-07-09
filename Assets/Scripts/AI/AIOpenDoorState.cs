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
         * agent._stateMachine.ChangeState(AIStateId.Idle) // ��� ���� ��ȯ�� idle���� �Ǵ��Ѵ�.
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
