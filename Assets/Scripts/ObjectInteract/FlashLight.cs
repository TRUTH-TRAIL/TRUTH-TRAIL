using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    public bool getFlash = false;
    //보유 배터리 수
    public int numBattery = 0;
    //최대 보유 배터리 수
    private int maxBattery = 5;
    [SerializeField]
    GameObject flashLight;
    private Light lightComponent;
    //배터리 잔량 표시 UI
    [SerializeField]
    private Slider batGaugeSlider;
    [SerializeField]
    private Text batText;

    //인벤토리 배터리 표시
    [SerializeField]
    Transform batteryGroup;

    //손전등 배터리
    private float batGauge;
    private int minBatGauge = 0;
    private int maxBatGauge = 10000;
    private float batGaugeTimer;
    private float batGaugeInterval = 1f;
    private float lowBat = 10f;
    private bool isFlashing = false;
    private Coroutine flashCoroutine;

    private Curses curse;

    public float battGauge { get { return batGauge; } set { batGauge = Mathf.Clamp(value, minBatGauge, maxBatGauge); } }

    private void Start()
    {
        batGauge = 100;
        batGaugeTimer = batGaugeInterval;
        lightComponent = flashLight.GetComponent<Light>();
        curse = GameObject.Find("CurseManager").GetComponent<Curses>();
    }
    private void Update()
    {
        TurnOnFlash();
        //BatGauge UI를 위한 임시 동작
        batGaugeSlider.value = batGauge;
        batText.text = batGauge.ToString("F1");
    }
    private void TurnOnFlash()
    {
        if (getFlash)
        {
            if (curse.activeCurse && curse.curseKey == 10)
            {
                flashLight.SetActive(false);
            }

            if (batGauge<=0)
                flashLight.SetActive(false);
            else
            {
                batGaugeSlider.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ToggleFlashLight(Color.white);
                    if (curse.activeCurse && curse.curseKey == 10)
                    {
                        flashLight.SetActive(false);
                        curse.die = true;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ToggleFlashLight(Color.red);
                    if (curse.activeCurse && curse.curseKey == 10)
                    {
                        flashLight.SetActive(false);
                        curse.die = true;
                    }
                }

                if (lightComponent.color == Color.white && flashLight.activeSelf)
                    UpdateBatGauge(1);
                else if (lightComponent.color == Color.red && flashLight.activeSelf)
                    UpdateBatGauge(3);
            }
            
        }
    }

    private void ToggleFlashLight(Color targetColor)
    {
        if (curse.activeCurse && curse.curseKey == 13)
        {
            if(lightComponent.color != targetColor) {
                flashLight.SetActive(false);
                curse.die = true;
            }
        }
        if (flashLight.activeSelf)
        {
            if (lightComponent.color == targetColor)
            {
                flashLight.SetActive(false);
            }
            else
            {
                lightComponent.color = targetColor;
            }
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
