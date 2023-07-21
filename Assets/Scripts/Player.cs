using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //FootGauge UI�� ���� �ӽ� ����

public class Player : MonoBehaviour
{
    //FootGauge UI ǥ�ø� ���� �ӽ� ����
    [SerializeField]
    private Slider footGaugeSlider;
    [SerializeField]
    private Text footText;

    private float speed = 4f;

    //�߼Ҹ� ������
    private float footGauge;  //���������� �ۿ�Ǵ� �߼Ҹ� ������
    private int minFootGauge = 0;
    private int maxFootGauge = 100;
    private float footGaugeTimer; 
    private float footGaugeIncreaseInterval = 1f;
    private float footGaugeDecreaseTimer = 2f;
    //Ư������
    bool activePaper = false;
    [SerializeField]
    GameObject paperPanel;

    // Start is called before the first frame update
    void Start()
    {
        footGaugeTimer = footGaugeIncreaseInterval;
        paperPanel.SetActive(activePaper);

    }

    // Update is called once per frame
    void Update()
    {
        FootStepGauge();
        ViewPaper();

        //FootGauge UI�� ���� �ӽ� ����
        footGaugeSlider.value = footGauge;
        footText.text = footGauge.ToString("F1");
    }

    private void FootStepGauge() //�߼Ҹ� ������ ����
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //����Ű �Է¿� ���� ������ �����ߴµ� w�����ε� �ٲ� �� ����
        {
            if (Input.GetKey(KeyCode.LeftShift)) //�޸� ��
            {
                UpdateFootGauge(1.5f*speed);
            }
            else //���� ��
            {
                UpdateFootGauge(1.0f * speed);
            }
            footGaugeDecreaseTimer = 2f;
        }
        else //������ ���� ��
        {
            UpdateFootGauge(-2.0f * speed);
        }
    }

    private void UpdateFootGauge(float n) //�߼Ҹ� ������ ����
    {
        if (n > 0)
        {
            footGaugeTimer -= Time.deltaTime;
            if (footGaugeTimer <= 0)
            {
                footGauge += n;
                footGauge = Mathf.Clamp(footGauge, minFootGauge, maxFootGauge);
                footGaugeTimer = footGaugeIncreaseInterval;
            }
        }
        else
        {
            footGaugeDecreaseTimer -= Time.deltaTime;
            if (footGaugeDecreaseTimer <= 0)
            {
                footGauge += n;
                footGauge = Mathf.Clamp(footGauge, minFootGauge, maxFootGauge);
                footGaugeDecreaseTimer = footGaugeIncreaseInterval;
            }
        }
    }

 
    private void ViewPaper() //Ư������ On/OFF
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            paperPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paperPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }


}
