using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChasePlayerState : AIState
{
    float timer = 0.0f;
    float chasingTime = 0.0f;
    float extraRotationSpeed = 30f;
    Collider[] colliders = new Collider[50];
    public AIStateId GetId()
    {
        return AIStateId.ChasePlayer;
    }

    public void Enter(AIAgent agent)
    {
        agent._navMeshAgent.speed = 2f;
        chasingTime = agent._config._maxChasingTime;
    }

    public void Exit(AIAgent agent)
    {
        
    }


    public void Update(AIAgent agent)
    {

        if (!agent.enabled)
            return;


        chasingTime -= Time.deltaTime;
        /**
         * if(Target is On My Sight){
         * ChasingTime = 0;
         * }
         */
        if(agent._sensor.Objects.Count > 0)
        {
            chasingTime = agent._config._maxChasingTime;
        }

        /**
         * if(agent.chasingTime > agent.config.MaxChasingTime) // 쫒고 있는 시간이 최대 쫒기 시간보다 낮으면 ChasePlayer State로 전환
         *  agent._stateMachine.ChangeState(AIStateId.IdleState); 
         */
        if (chasingTime < 0)
            agent._stateMachine.ChangeState(AIStateId.Idle);

        LookTarget(agent);
        ChaseTarget(agent);
    }


    private void ChaseTarget(AIAgent agent)
    {
        timer -= Time.deltaTime;

        if (!agent._navMeshAgent.hasPath)
        {
            agent._navMeshAgent.destination = agent._playerTransform.position;
        }

        if (timer < 0.0f)
        {
            
            float sqDistance = (agent._playerTransform.position - agent._navMeshAgent.destination).sqrMagnitude;

            if (sqDistance > agent._config._maxDistance * agent._config._maxDistance)
            {
                if (agent._navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    Debug.Log("PathPartial");
                    agent._navMeshAgent.destination = agent._playerTransform.position;
                }
                else
                {
                    Debug.Log("Not Partial");
                    agent._navMeshAgent.SetDestination(agent._playerTransform.position);
                }
            }

            timer = agent._config._maxTime;
        }

        if (IsCatchPlayer(agent))
        {
            agent._stateMachine.ChangeState(AIStateId.AttackPlayer);
        }
    }

    private void LookTarget(AIAgent agent)
    {
        Vector3 lookrotation = agent._playerTransform.position - agent.transform.position;
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(new Vector3(lookrotation.x, 0, lookrotation.z)), extraRotationSpeed * Time.deltaTime);
    }
    private bool IsCatchPlayer(AIAgent agent)
    {
        float distance = (agent._playerTransform.position - agent.transform.position).sqrMagnitude;
        if(distance < agent._config._catchDistance * agent._config._catchDistance && !Physics.Linecast(agent.transform.position, agent._playerTransform.position)){
            return true;
        }
        return false;
    }


}
