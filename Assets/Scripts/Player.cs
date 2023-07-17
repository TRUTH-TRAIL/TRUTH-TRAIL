using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //�߼Ҹ� ������
    private float footGauge;  //���������� �ۿ�Ǵ� �߼Ҹ� ������
    private int minFootGauge = 0;
    private int maxFootGauge = 100;
    private float footGaugeTimer; 
    private float footGaugeIncreaseInterval = 1f;

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
    }

    private void FootStepGauge() //�߼Ҹ� ������ ����
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //����Ű �Է¿� ���� ������ �����ߴµ� w�����ε� �ٲ� �� ����
        {
            if (Input.GetKey(KeyCode.LeftShift)) //�޸� ��
            {
                UpdateFootGauge(1.5f);
            }
            else //���� ��
            {
                UpdateFootGauge(1.0f);
            }
        }
        else //������ ���� ��
        {
            UpdateFootGauge(-2.0f);
        }
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

 
    private void ViewPaper() //Ư������ On/OFF
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activePaper = !activePaper;
            paperPanel.SetActive(activePaper);
        }
    }
}
