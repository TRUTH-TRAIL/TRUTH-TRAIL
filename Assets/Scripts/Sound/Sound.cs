using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioClip[] clip;
    private AudioSource audioSource;

    float timer;
    float timer2;
    float waitingTIme;
    float waitingTimeAlley;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        timer = 0.0f;
        timer2 = 0.0f;
        waitingTIme = 0.5f;
        waitingTimeAlley = 50f;
        bleaknessSound();
    }

    void Update()
    {
        //음산한소리
        timer2 += Time.deltaTime;
        if (timer > waitingTimeAlley)
        {
            bleaknessSound();
            timer2 = 0;
        }
        
        //발소리
        timer += Time.deltaTime;
        if(timer > waitingTIme)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                footStepSound();
            }
            timer = 0;
        }
        
    }


//일반 버튼
void buttonSound()
    {
        audioSource.clip = clip[0];
        audioSource.Play();
    }

    //뒤로가기 버튼
    void buttonBackSound()
    {
        audioSource.clip = clip[1];
        audioSource.Play();
    }

    //앨리가 주변에 있을때 음산함 사운드
    void bleaknessSound()
    {
        audioSource.clip = clip[2];
        audioSource.Play();
    }

    //앨리 플레이어 발견, 추격모드 돌입
    void alleyFoundSound()
    {
        audioSource.clip = clip[3];
        audioSource.Play();
    }

    //앨리 비명소리_사망컷씬
    void alleyAtackSound()
    {
        audioSource.clip = clip[4];
        audioSource.Play();
    }

    //서랍열기
    void drawerSound()
    {
        audioSource.clip = clip[5];
        audioSource.Play();
    }

    //가림천 올리기
    void clothSound()
    {
        audioSource.clip = clip[6];
        audioSource.Play();
    }

    //문 손잡이
    void doorOpenHandleSound()
    {
        audioSource.clip = clip[7];
        audioSource.Play();
    }

    //문 열기
    void doorOpenSound2()
    {
        audioSource.clip = clip[8];
        audioSource.Play();
    }

    //문 닫기
    void doorCloseSound()
    {
        audioSource.clip = clip[9];
        audioSource.Play();
    }

    //키로 문 열기
    void keyOpenSound()
    {
        audioSource.clip = clip[10];
        audioSource.Play();
    }

    //해독 정화
    void purificationSound()
    {
        audioSource.clip = clip[11];
        audioSource.Play();
    }

    //라이터 켜기
    void lighterSound()
    {
        audioSource.clip = clip[12];
        audioSource.Play();
    }

    //라이터 불 지피기
    void lighterFireSound()
    {
        audioSource.clip = clip[13];
        audioSource.Play();
    }

    //발소리
    void footStepSound()
    {
        audioSource.clip = clip[14];
        audioSource.Play();
        /*
        audioSource.clip = clip[15];
        audioSource.Play();
        audioSource.clip = clip[16];
        audioSource.Play();
        audioSource.clip = clip[17];
        audioSource.Play();
        audioSource.clip = clip[18];
        audioSource.Play();
        audioSource.clip = clip[19];
        audioSource.Play();
        */
    }
}