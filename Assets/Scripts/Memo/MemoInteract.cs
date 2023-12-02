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
                Instantiate(lighterPrefab, new Vector3(272.6704f, 10.986f,263.4312f), Quaternion.identity);
                break;
            case 2:
                Instantiate(skullBonePrefab, new Vector3(284.69101f,3.87914777f,267.557007f), Quaternion.identity);
                break;
            case 3:
                cross = Instantiate(crossPrefab, new Vector3(250.888f,10.8710003f,261.863007f), Quaternion.identity);
                cross.localScale = new Vector3(6.0f,6.0f,6.0f);
                cross.eulerAngles = new Vector3(0f,270f,315f);
                break;
        }
    }
}


