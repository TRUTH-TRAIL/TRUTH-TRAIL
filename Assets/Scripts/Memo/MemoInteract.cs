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
                Instantiate(lighterPrefab, new Vector3(274.373566f,6.76346731f,252.518921f), Quaternion.identity);
                
                break;
            case 2:
                Instantiate(skullBonePrefab, new Vector3(274.356293f, 6.7685895f, 253.368256f), Quaternion.identity);
                break;
            case 3:
                Instantiate(crossPrefab, new Vector3(274.253479f, 6.76313066f, 253.621323f), Quaternion.identity);
                break;
        }
    }
}
