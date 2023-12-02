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
        //�����ѼҸ�
        timer2 += Time.deltaTime;
        if (timer > waitingTimeAlley)
        {
            bleaknessSound();
            timer2 = 0;
        }
        
        //�߼Ҹ�
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


//�Ϲ� ��ư
void buttonSound()
    {
        audioSource.clip = clip[0];
        audioSource.Play();
    }

    //�ڷΰ��� ��ư
    void buttonBackSound()
    {
        audioSource.clip = clip[1];
        audioSource.Play();
    }

    //�ٸ��� �ֺ��� ������ ������ ����
    void bleaknessSound()
    {
        audioSource.clip = clip[2];
        audioSource.Play();
    }

    //�ٸ� �÷��̾� �߰�, �߰ݸ�� ����
    void alleyFoundSound()
    {
        audioSource.clip = clip[3];
        audioSource.Play();
    }

    //�ٸ� ���Ҹ�_����ƾ�
    void alleyAtackSound()
    {
        audioSource.clip = clip[4];
        audioSource.Play();
    }

    //��������
    void drawerSound()
    {
        audioSource.clip = clip[5];
        audioSource.Play();
    }

    //����õ �ø���
    void clothSound()
    {
        audioSource.clip = clip[6];
        audioSource.Play();
    }

    //�� ������
    void doorOpenHandleSound()
    {
        audioSource.clip = clip[7];
        audioSource.Play();
    }

    //�� ����
    void doorOpenSound2()
    {
        audioSource.clip = clip[8];
        audioSource.Play();
    }

    //�� �ݱ�
    void doorCloseSound()
    {
        audioSource.clip = clip[9];
        audioSource.Play();
    }

    //Ű�� �� ����
    void keyOpenSound()
    {
        audioSource.clip = clip[10];
        audioSource.Play();
    }

    //�ص� ��ȭ
    void purificationSound()
    {
        audioSource.clip = clip[11];
        audioSource.Play();
    }

    //������ �ѱ�
    void lighterSound()
    {
        audioSource.clip = clip[12];
        audioSource.Play();
    }

    //������ �� ���Ǳ�
    void lighterFireSound()
    {
        audioSource.clip = clip[13];
        audioSource.Play();
    }

    //�߼Ҹ�
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