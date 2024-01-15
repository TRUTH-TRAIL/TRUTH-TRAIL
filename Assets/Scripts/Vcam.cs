using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vcam : MonoBehaviour
{
    private Vector3 endPosition = new Vector3(0, 3.9f, 4.0f);
    private float speed = 1.0f; 

    private bool isMoving = false; // 카메라 이동 상태를 나타내는 플래그

    void OnEnable()
    {
        // 오브젝트가 활성화될 때 이동을 시작
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveCamera();
        }
    }


    void MoveCamera()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPosition, speed * Time.deltaTime);
    if (transform.localPosition == endPosition)
    {
        isMoving = false;
    }
    }
}
