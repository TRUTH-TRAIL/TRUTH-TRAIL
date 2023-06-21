using UnityEngine;
using UnityEngine.AI;   // 스크립트에서 내비게이션 시스템 기능을 사용하려면 AI 네임스페이스를 using 선언해야함

public class Moveable : MonoBehaviour
{
    // 길을 찾아서 이동할 에이전트
    NavMeshAgent agent;
    Animator anime;

    // 에이전트의 목적지
    [SerializeField]
    Transform target;

    private void Awake()
    {
        // 게임이 시작되면 게임 오브젝트에 부착된 NavMeshAgent 컴포넌트를 가져와서 저장
        agent = GetComponent<NavMeshAgent>();
        anime = GetComponent<Animator>();
    }

    void Update()
    {
        if ((target.position - this.transform.position).magnitude < agent.stoppingDistance)
        {
            anime.SetBool("isStop", true);
            return;
        }
        anime.SetBool("isStop", false);
        agent.SetDestination(target.position);
    }
}