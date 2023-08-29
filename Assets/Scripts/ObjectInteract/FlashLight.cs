using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    public bool getFlash = false;
    //���� ���͸� ��
    public int numBattery = 0;
    //�ִ� ���� ���͸� ��
    private int maxBattery = 5;
    [SerializeField]
    GameObject flashLight;
    private Light lightComponent;
    //���͸� �ܷ� ǥ�� UI
    [SerializeField]
    private Slider batGaugeSlider;
    [SerializeField]
    private Text batText;

    //�κ��丮 ���͸� ǥ��
    [SerializeField]
    Transform batteryGroup;

    //������ ���͸�
    private float batGauge;
    private int minBatGauge = 0;
    private int maxBatGauge = 100;
    private float batGaugeTimer;
    private float batGaugeInterval = 1f;
    private float lowBat = 10f;
    private bool isFlashing = false;
    private Coroutine flashCoroutine;


    public float battGauge { get { return batGauge; } set { batGauge = Mathf.Clamp(value, minBatGauge, maxBatGauge); } }

    private void Start()
    {
        batGauge = 100;
        batGaugeTimer = batGaugeInterval;
        lightComponent = flashLight.GetComponent<Light>();
    }
    private void Update()
    {
        TurnOnFlash();
        //BatGauge UI�� ���� �ӽ� ����
        batGaugeSlider.value = batGauge;
        batText.text = batGauge.ToString("F1");
    }
    private void TurnOnFlash()
    {
        if (getFlash)
        {
            if(batGauge<=0)
                flashLight.SetActive(false);
            else
            {
                batGaugeSlider.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    ToggleFlashLight(Color.white);

                if (Input.GetKeyDown(KeyCode.Alpha2))
                    ToggleFlashLight(Color.red);

                if (lightComponent.color == Color.white && flashLight.activeSelf)
                    UpdateBatGauge(1);
                else if (lightComponent.color == Color.red && flashLight.activeSelf)
                    UpdateBatGauge(3);
            }
            
        }
    }

    private void ToggleFlashLight(Color targetColor)
    {
        if (flashLight.activeSelf)
        {
            if (lightComponent.color == targetColor)
                flashLight.SetActive(false);
            else
                lightComponent.color = targetColor;
        }
        else
        {
            lightComponent.color = targetColor;
            flashLight.SetActive(true);
        }

        

    }

    private void UpdateBatGauge(float n)
    {
        batGaugeTimer -= Time.deltaTime;
        
        if (batGaugeTimer <= 0)
        {
            batGauge -= n;
            batGauge = Mathf.Clamp(batGauge, minBatGauge, maxBatGauge);
            batGaugeTimer = batGaugeInterval;
        }
        if(batGauge < lowBat && !isFlashing)
        {
            flashCoroutine = StartCoroutine(FlashWhenLowBattery());
        }
       
    }
    IEnumerator FlashWhenLowBattery()
    {
        isFlashing = true;
        Color curColor;
        while(batGauge < lowBat && flashLight.activeSelf)
        {
            curColor = lightComponent.color;
            lightComponent.color = Color.black;
            yield return new WaitForSeconds(0.5f);
            lightComponent.color = curColor;
            yield return new WaitForSeconds(0.5f);
        }

        isFlashing = false;
        flashCoroutine = null;
    }


    public void UpdateBattery(bool useBat)
    {
        if (useBat)
        {
            if (numBattery > 0)
            {
                numBattery -= 1;
                if (batGauge < maxBatGauge - 30)
                    batGauge += 30f;
                else
                    batGauge = maxBatGauge;
            }
        }
        else
        {
            if(numBattery<maxBattery)
                numBattery += 1;
        }
        for (int i = 0; i < maxBattery; i++)
        {
            batteryGroup.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < numBattery; i++)
        {
            batteryGroup.GetChild(i).gameObject.SetActive(true);
        }

    }
}
