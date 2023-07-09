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
        
    }

    public void Exit(AIAgent agent)
    {

    }



    public void Update(AIAgent agent)
    {
        /**
         * 다음 State 판단
         * Chase 판단
         * if(agent.chasingTime < agent.config.MaxChasingTime) // 쫒고 있는 시간이 최대 쫒기 시간보다 낮으면 ChasePlayer State로 전환
         *  agent._stateMachine.ChangeState(AIStateId.ChasePlayer); 
         **/
        if (agent._sensor.IsInSight(agent._playerTransform.gameObject))
        {
            agent._stateMachine.ChangeState(AIStateId.ChasePlayer);
        }
        /** 
        * Find 판단
        * else if(aggroGauge >= 1) //어그로게이지가 1 이상이면 FindPlayer State로 전환
        * {agent._stateMachine.ChangeState(AIStateId.FindPlayer); } // 어그로게이지 판단
        **/

        /** Move 판단
        * else //위에 아무것도 해당하지 않는다면 Move State로 변환
        * agent._stateMachine.ChangeState(AIStateId.Move);
        **/



    }
}
