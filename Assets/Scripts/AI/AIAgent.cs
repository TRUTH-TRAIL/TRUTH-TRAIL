using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateId _initialState;
    public AIAgentConfig _config;

    [HideInInspector] public Transform _playerTransform;
    [HideInInspector] public AIStateMachine _stateMachine;
    
    [HideInInspector] public NavMeshAgent _navMeshAgent;
    
    [HideInInspector] public AISensor _sensor;

    bool isCatchPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        SetNavMeshAgent();
        _stateMachine = new AIStateMachine(this);
        _sensor = GetComponent<AISensor>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _stateMachine.RegisterState(new AIChasePlayerState());
        _stateMachine.RegisterState(new AIIdleState());
        _stateMachine.RegisterState(new AIFindState());
        _stateMachine.RegisterState(new AIOpenDoorState());
        _stateMachine.RegisterState(new AIAttackPlayerState());
        _stateMachine.ChangeState(_initialState);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Update();
    }
    
    private void SetNavMeshAgent()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void OnDrawGizmos()
    {
        if(_stateMachine?.currentState == AIStateId.ChasePlayer)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
