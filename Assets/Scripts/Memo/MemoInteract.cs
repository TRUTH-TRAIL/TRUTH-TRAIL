using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoInteract : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform crossPrefab;
    public Transform skullBonePrefab;
    public Transform lighterPrefab;
    private Transform cross;
    public void ObjectAppear(int num)
    {
        switch (num)
        {
            case 0:
                break;
            case 1:
                Instantiate(lighterPrefab, new Vector3(266.218994f,6.02400017f,250.612f), Quaternion.identity);
                break;
            case 2:
                Instantiate(skullBonePrefab, new Vector3(260.589996f,0.344999999f,284.359985f), Quaternion.identity);
                break;
            case 3:
                cross = Instantiate(crossPrefab, new Vector3(266.18399f,6.42500019f,264.684998f), Quaternion.identity);
                cross.localScale = new Vector3(4.0f,4.0f,4.0f);
                cross.eulerAngles = new Vector3(0f,270f,350f);
                break;
        }
    }
}


