using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //발소리 게이지
    private float footGauge;  //내부적으로 작용되는 발소리 게이지
    private int minFootGauge = 0;
    private int maxFootGauge = 100;
    private float footGaugeTimer; 
    private float footGaugeIncreaseInterval = 1f;

    //특수용지
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

    private void FootStepGauge() //발소리 게이지 관리
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //방향키 입력에 따라 게이지 설정했는데 w만으로도 바꿀 수 있음
        {
            if (Input.GetKey(KeyCode.LeftShift)) //달릴 때
            {
                UpdateFootGauge(1.5f);
            }
            else //걸을 때
            {
                UpdateFootGauge(1.0f);
            }
        }
        else //가만히 있을 때
        {
            UpdateFootGauge(-2.0f);
        }
    }

    private void UpdateFootGauge(float n) //발소리 게이지 조절
    {
        footGaugeTimer -= Time.deltaTime;
        if (footGaugeTimer <= 0)
        {
            footGauge += n;
            footGauge = Mathf.Clamp(footGauge, minFootGauge, maxFootGauge);
            footGaugeTimer = footGaugeIncreaseInterval;
        }
    }

 
    private void ViewPaper() //특수용지 On/OFF
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            activePaper = !activePaper;
            paperPanel.SetActive(activePaper);
        }
    }
}
