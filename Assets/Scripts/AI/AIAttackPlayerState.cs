using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackPlayerState : AIState
{
    public AIStateId GetId()
    {
        return AIStateId.AttackPlayer;
    }


    public void Enter(AIAgent agent)
    {
        /**
         * Start CutScene;
         * if(Cutcene == end) Show GameOverScene;
         */
        Debug.Log("Catch1");
        agent._navMeshAgent.isStopped = true;
        GameOverUI gameOverUI = Transform.FindObjectOfType<GameOverUI>(true);
        gameOverUI.gameObject.SetActive(true);
    }

    public void Update(AIAgent agent)
    {

    }

    public void Exit(AIAgent agent)
    {
        agent._navMeshAgent.isStopped = false;
    }


}
