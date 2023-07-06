using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 previousPosition;
    private float footGauge; //�߼Ҹ�������
    private int minFootGauge = 0;
    private int maxFootGauge = 100;
    private float footGaugeTimer; 
    private float footGaugeIncreaseInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        footGaugeTimer = footGaugeIncreaseInterval;
    }

    // Update is called once per frame
    void Update()
    {
        FootStepGauge();
    }

    private void FootStepGauge() //�߼Ҹ� ������ ����
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                UpdateFootGauge(1.5f);
            }
            else
            {
                UpdateFootGauge(1.0f);
            }
        }
        else
        {
            UpdateFootGauge(-2.0f);
        }
        previousPosition = transform.position;
    }

    private void UpdateFootGauge(float n) //�߼Ҹ� ������ ����
    {
        footGaugeTimer -= Time.deltaTime;
        if (footGaugeTimer <= 0)
        {
            footGauge += n;
            footGauge = Mathf.Clamp(footGauge, minFootGauge, maxFootGauge);
            footGaugeTimer = footGaugeIncreaseInterval;
        }
    }
}
