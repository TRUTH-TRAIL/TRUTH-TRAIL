using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vcam : MonoBehaviour
{
    private Vector3 endPosition = new Vector3(0, 3.9f, 4.0f);
    private float speed = 1.0f; 

    private bool isMoving = false; // ī�޶� �̵� ���¸� ��Ÿ���� �÷���

    void OnEnable()
    {
        // ������Ʈ�� Ȱ��ȭ�� �� �̵��� ����
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
