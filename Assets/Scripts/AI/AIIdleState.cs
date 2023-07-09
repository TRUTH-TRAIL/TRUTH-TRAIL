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
         * ���� State �Ǵ�
         * Chase �Ǵ�
         * if(agent.chasingTime < agent.config.MaxChasingTime) // �i�� �ִ� �ð��� �ִ� �i�� �ð����� ������ ChasePlayer State�� ��ȯ
         *  agent._stateMachine.ChangeState(AIStateId.ChasePlayer); 
         **/
        if (agent._sensor.IsInSight(agent._playerTransform.gameObject))
        {
            agent._stateMachine.ChangeState(AIStateId.ChasePlayer);
        }
        /** 
        * Find �Ǵ�
        * else if(aggroGauge >= 1) //��׷ΰ������� 1 �̻��̸� FindPlayer State�� ��ȯ
        * {agent._stateMachine.ChangeState(AIStateId.FindPlayer); } // ��׷ΰ����� �Ǵ�
        **/

        /** Move �Ǵ�
        * else //���� �ƹ��͵� �ش����� �ʴ´ٸ� Move State�� ��ȯ
        * agent._stateMachine.ChangeState(AIStateId.Move);
        **/



    }
}
