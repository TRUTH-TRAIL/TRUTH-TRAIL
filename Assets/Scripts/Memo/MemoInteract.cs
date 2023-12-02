using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoInteract : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform crossPrefab;
    public Transform skullBonePrefab;
    public Transform lighterPrefab;
    public void ObjectAppear(int num)
    {
        switch (num)
        {
            case 0:
                break;
            case 1:
                Instantiate(lighterPrefab, new Vector3(272.6704f, 10.986f,263.4312f), Quaternion.identity);
                break;
            case 2:
                Instantiate(skullBonePrefab, new Vector3(-128.798f, -342.6276f, 267.591f), Quaternion.identity);
                break;
            case 3:
                Instantiate(crossPrefab, new Vector3(249f, 10f, 263f), Quaternion.identity);
                break;
        }
    }
}
