using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //FootGauge UI를 위한 임시 적용

public class Player : MonoBehaviour
{
    public GameObject escB;
    private float speed = 4f;
    //public RaycastHit hitData;
    //발소리 게이지
    private float footGauge;  //내부적으로 작용되는 발소리 게이지
    private int minFootGauge = 0;
    private int maxFootGauge = 100;
    private float footGaugeTimer; 
    private float footGaugeIncreaseInterval = 1f;
    private float footGaugeDecreaseTimer = 2f;
    public float FootGauge { get { return footGauge; } set { footGauge = Mathf.Clamp(value, minFootGauge, maxFootGauge); } }
    //특수용지
    bool activePaper = false;
    [SerializeField]
    GameObject paperPanel;
    [SerializeField]
    GameObject gameoverui;
    // Start is called before the first frame update
    void Start()
    {
        footGaugeTimer = footGaugeIncreaseInterval;
        paperPanel.SetActive(activePaper);
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.DrawRay(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenPointToRay));
        /*float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        Debug.DrawRay(transform.position, dir, Color.red, 0.3f);
        if(Physics.Raycast(transform.position, transform.forward, out hitData, 15f)){
            Debug.Log(hitData.collider.name);
        }*/
        FootStepGauge();
        //ViewPaper();
    }

    private void FootStepGauge() //발소리 게이지 관리
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) //방향키 입력에 따라 게이지 설정했는데 w만으로도 바꿀 수 있음
        {
            if (Input.GetKey(KeyCode.LeftShift)) //달릴 때
            {
                UpdateFootGauge(1.5f*speed);
            }
            else //걸을 때
            {
                UpdateFootGauge(1.0f * speed);
            }
            footGaugeDecreaseTimer = 2f;
        }
        else //가만히 있을 때
        {
            UpdateFootGauge(-2.0f * speed);
        }
    }

    private void UpdateFootGauge(float n) //발소리 게이지 조절
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

 
    private void ViewPaper() //특수용지 On/OFF
    {
        if(gameoverui.activeSelf != true){
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
}
