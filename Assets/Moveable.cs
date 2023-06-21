using UnityEngine;
using UnityEngine.AI;   // ��ũ��Ʈ���� ������̼� �ý��� ����� ����Ϸ��� AI ���ӽ����̽��� using �����ؾ���

public class Moveable : MonoBehaviour
{
    // ���� ã�Ƽ� �̵��� ������Ʈ
    NavMeshAgent agent;
    Animator anime;

    // ������Ʈ�� ������
    [SerializeField]
    Transform target;

    private void Awake()
    {
        // ������ ���۵Ǹ� ���� ������Ʈ�� ������ NavMeshAgent ������Ʈ�� �����ͼ� ����
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