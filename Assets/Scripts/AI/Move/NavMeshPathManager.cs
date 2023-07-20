using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPathManager
{
    Transform[] nodes;
    bool[] visited;
    public int curDestination;

    public NavMeshPathManager()
    {
        SetPath();
    }

    public void SetPath()
    {
        Transform tf = GameObject.FindObjectOfType<IsLocation>().transform;
        nodes = new Transform[tf.childCount];
        Debug.Log($"ChildCount : {tf.childCount}");
        for (int i = 0; i < tf.childCount; i++)
        {
            nodes[i] = tf.GetChild(i);
            Debug.Log($"{i} : {nodes[i].name}");
        }

        visited = new bool[tf.childCount];
    }
    public int GetShortestPathNumber(NavMeshAgent agent)
    {

        if (visited.Length <= 0)
            return -1;
        int shortestPathNumber = 0;
        float shortestPathLength = float.MaxValue;
        int visitCount = 0;

        int i = 0;
        var path = new NavMeshPath();
        while (i < nodes.Length)
        {
            if (visited[i])
            {
                visitCount++;
                i++;
                continue;
            }

            if (agent.CalculatePath(nodes[i].position, path))
            {
                float length = 0.0f;
                var prevCorner = agent.transform.position;
                foreach (var corner in path.corners)
                {
                    length += (prevCorner - corner).sqrMagnitude;
                    prevCorner = corner;
                }

                if (shortestPathLength > length)
                {
                    shortestPathNumber = i;
                    shortestPathLength = length;
                }
            }
            i++;
            if (visitCount == visited.Length)
            {
                Clear();
                i = 0;
            }
        }

        return shortestPathNumber;
    }

    public void SetShortestDestination(NavMeshAgent agent)
    {
        int shorTestDestination = GetShortestPathNumber(agent);

        if (shorTestDestination < 0)
            return;
        agent.SetDestination(nodes[shorTestDestination].position);
        curDestination = shorTestDestination;

    }

    public Dictionary<int, Transform> ShowNodes()
    {
        Dictionary<int, Transform> nodeDic = new Dictionary<int, Transform>();
        for (int i = 0; i < nodes.Length; i++)
        {
            nodeDic.Add(i, nodes[i]);
        }

        return nodeDic;
    }

    public bool[] ShowNodeVisit()
    {
        return visited;
    }

    public void SetVisit(bool boolean)
    {
        visited[curDestination] = boolean;
    }

    public void Clear()
    {
        for (int i = 0; i < visited.Length; i++)
        {
            visited[i] = false;
        }
    }
}
