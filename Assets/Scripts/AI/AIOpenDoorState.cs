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
        Debug.Log("Door Find");
        agent._navMeshAgent.isStopped = true;
        agent._navMeshAgent.velocity = Vector3.zero;
        if (CheckDoor(agent))
        {
            Debug.Log("Door Animation");
            agent._animator.Play("OpenDoor",-1, 0f);
            door.OpenDoor(agent.transform);
        }
        else { agent._stateMachine.ChangeState(AIStateId.Idle); }
    }

    public void Update(AIAgent agent)
    {
        if (door.Open && agent._animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor"))
        {
            Debug.Log(agent._animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if(agent._animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                door = null; 
                agent._animator.Play("Idle And Walk", -1, 0);
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
        if (Physics.Raycast(agent.transform.position + new Vector3(0, 0.5f, 0), agent.transform.forward, out hit, 0.5f, 1 << LayerMask.NameToLayer("Door")))
        {
            door = hit.transform.GetComponent<Door>();
            if (!door.Open)
            {
                return true;
            }
        }
        return false;
    }
}
