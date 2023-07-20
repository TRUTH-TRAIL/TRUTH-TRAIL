using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateId _initialState;
    public AIAgentConfig _config;
    public Animator _animator;

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
        _animator = GetComponent<Animator>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _stateMachine.RegisterState(new AIChasePlayerState());
        _stateMachine.RegisterState(new AIIdleState());
        _stateMachine.RegisterState(new AIFindState());
        _stateMachine.RegisterState(new AIOpenDoorState());
        _stateMachine.RegisterState(new AIAttackPlayerState());
        _stateMachine.RegisterState(new AIMoveState());
        _stateMachine.ChangeState(_initialState);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($" currentState : {_stateMachine.currentState}");
        if (_stateMachine.currentState != AIStateId.OpenDoor)
            CheckDoor();

        _stateMachine.Update();
    }
    
    private void SetNavMeshAgent()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void CheckPlayerIsInSight()
    {

    }
    public void CheckDoor()
    {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position + new Vector3(0, 0.5f, 0), transform.forward, out hit, 0.5f, 1 << LayerMask.NameToLayer("Door")))
        {
            Door door = hit.transform.GetComponent<Door>();
            if (!door.Open)
            {
                _stateMachine.ChangeState(AIStateId.OpenDoor);
            }
                
        }
    }

    private void OnDrawGizmos()
    {
        if(_stateMachine?.currentState == AIStateId.ChasePlayer)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(this.transform.position + new Vector3(0, 0.5f, 0), 0.5f);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position + new Vector3(0, 0.5f, 0), this.transform.forward*0.5f);
    }
}
