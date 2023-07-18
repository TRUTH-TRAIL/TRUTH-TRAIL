﻿using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    bool trig, open;
    public float smooth = 2.0f;
    public float DoorOpenAngle = 90.0f;
    private Quaternion defaultRot;
    private Quaternion openRot;
    public Text txt;
    public Transform player;

    void Start()
    {
        defaultRot = transform.rotation;
        openRot = Quaternion.Euler(defaultRot.eulerAngles.x, defaultRot.eulerAngles.y + DoorOpenAngle, defaultRot.eulerAngles.z);
        txt = GameObject.FindObjectOfType<Text>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (open)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRot, Time.deltaTime * smooth);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRot, Time.deltaTime * smooth);
        }

        if (Input.GetMouseButtonDown(0) && trig)
        {
            if (Vector3.Dot(transform.right, player.position - transform.position) > 0)
            {
                openRot = Quaternion.Euler(defaultRot.eulerAngles.x, defaultRot.eulerAngles.y + DoorOpenAngle, defaultRot.eulerAngles.z);
            }
            else
            {
                openRot = Quaternion.Euler(defaultRot.eulerAngles.x, defaultRot.eulerAngles.y - DoorOpenAngle, defaultRot.eulerAngles.z);
            }
            open = !open;
        }

        if (trig)
        {
            if (open)
            {
                txt.text = "문 닫기(클릭)";
            }
            else
            {
                txt.text = "문 열기(클릭)";
            }
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (!open)
            {
                txt.text = "문 닫기(클릭)";
            }
            else
            {
                txt.text = "문 열기(클릭)";
            }
            trig = true;
        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            txt.text = " ";
            trig = false;
        }
    }
}
