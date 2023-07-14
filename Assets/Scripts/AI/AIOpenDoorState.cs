using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpenDoorState : AIState
{
    Door door;
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
        agent._navMeshAgent.isStopped = true;
        if (!CheckDoor(agent))
        {
            agent._animator.Play("OpenDoor");
            door.Open = true;
        }
    }

    public void Update(AIAgent agent)
    {
        if (!door.Open)
        {
            if(agent._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                agent._animator.Play("Idle And Walk");
                agent._stateMachine.ChangeState(AIStateId.Idle);
            }
        }
    }

    public void Exit(AIAgent agent)
    {
        agent._navMeshAgent.isStopped = false;
    }

    public bool CheckDoor(AIAgent agent)
    {
        RaycastHit hit;
        if (Physics.Raycast(agent.transform.position + new Vector3(0, 0.5f, 0), agent.transform.forward + new Vector3(0, 0.5f, 0), out hit, 1f, LayerMask.NameToLayer("Door")))
        {
            door = hit.transform.GetComponent<Door>();
            if (!door.Open)
            {
                return true;
            }
        }
        return false;
    }

    public void OpenDoorIsOver()
    {
        
    }
}
