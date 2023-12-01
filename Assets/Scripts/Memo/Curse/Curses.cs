using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Curses : MonoBehaviour
{
    public bool activeCurse = false;
    public int curseKey = 0;
    private bool activeTimer = false;
    private Coroutine curseTimerCoroutine;
    public bool die = false;
    //�ܼ� ���� �� ������ ��
    //curse.json���Ͽ��� �������� ���� �о����
    private List<Curse> readCurse;
    private int CurseCount = 0;
    public int doorCurseCount = 0;
    [SerializeField] GameObject player;
    [SerializeField] Text curseText;
    private PlayerCrash playerCrash;
    private ChangeCamera changeCamera;
    private bool ending = false;

    private void Start()
    {
        playerCrash = player.GetComponent<PlayerCrash>();
        changeCamera = player.transform.GetChild(1).GetComponent<ChangeCamera>();
    }
    public string ActiveCurse()
    {
        int RandomCurse;
        string retString;
        activeCurse = true;
        readCurse = MemoReader.ReadCurse();
        CurseCount = readCurse.Count;
        RandomCurse = Random.Range(0, CurseCount);
        //RandomCurse = 8; //Test�� json���Ͽ� �� ������.
        retString = readCurse[RandomCurse].GetCurseData();
        curseKey = readCurse[RandomCurse].GetKey();
        readCurse.RemoveAt(RandomCurse);
        return retString;
    }

    public void ClaerCurse()
    {
        activeCurse = false;
        curseKey = -1;
        curseText.gameObject.SetActive(false);
    }
    void Update()
    {
        if(activeCurse)
        {
            if(curseKey>=0 && curseKey <= 9)
                BasicCurse();
            else if(curseKey==10)
                TimeCurse(120.0f);
            else if(curseKey==11)
                TimeCurse(210.0f);
            else if (curseKey==12)
                TimeCurse(60.0f);
            else if( curseKey==13)
                TimeCurse(300.0f);
            else if(curseKey==14)
                TimeCurse(60.0f);
            else if(curseKey==15)
                TimeCurse(150.0f);
            else if(curseKey==16)
                TimeCurse(180.0f);
            else if(curseKey==17)
                TimeCurse(180.0f);
            else if(curseKey==18)
                TimeCurse(180.0f);
        }
        if(!ending&&die){
            Debug.Log("���@@@@@");//���� ���ó�� �Լ� ȣ��
            changeCamera.SwitchToVirtualCamera();
            ending=true;
        }
    }

    //die������ �߰��ؼ� �� ���ָ��� �Լ��� ����� ���� �ƴ϶� �⺻, Ÿ�� �����θ� ������ �Լ� ����� ���� ���ڴ�.
    //�� �κ��� �ڵ� �����ϸ鼭 �����ؾ߰ڴ�.
    //������ �ȴٸ� switch case�� ������ ������ �ʿ� ���� if������ �����ϰ� ��Ÿ�� �� �ִ�.
    //���� �⺻ ������ ��� die�� ��� ���, time ������ ��� ������ �޾� �ڷ�ƾ ���� �� die�� ��� ������� �θ� �ǰڴ�.
    //�����غ��� ������ �Լ��� �ʿ� ���ڴ�. �׳� if������ �����ϰ� �� �� ���� �� ����.

    private void BasicCurse()
    {
        if (die)
        {
            Debug.Log("���@@@@@");//���� ���ó�� �Լ� ȣ��
            changeCamera.SwitchToVirtualCamera();
            ending=true;
            ClaerCurse();
        }
    }
    private void TimeCurse(float timer)
    {
        if (!activeTimer)
        {
            curseTimerCoroutine = StartCoroutine(CurseTimer(timer));
        }

        if (die)
        {
            Debug.Log("���@@@@@");//���� ���ó�� �Լ� ȣ��
            ending=true;
            changeCamera.SwitchToVirtualCamera();
            ClearTimer();
            LoadingScene.Instance.LoadScene("GameOver");
        }
    }



    IEnumerator CurseTimer(float curseTime)
    {
        activeTimer = true;
        float curTime = curseTime;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("���@@@@@");//�ð� ������ ��� ���� ���ó�� �Լ� ȣ��
        ending=true;
        changeCamera.SwitchToVirtualCamera();
    }
    private void ClearTimer()
    {
        if (activeTimer)
        {
            StopCoroutine(curseTimerCoroutine);
            ClaerCurse();
            activeTimer = false;
        }
    }

}
