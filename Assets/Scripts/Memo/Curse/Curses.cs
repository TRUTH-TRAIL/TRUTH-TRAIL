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
    public bool activeTimer = false;
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
    public int countTimer;

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
        //RandomCurse = 19; //Test�� json���Ͽ� �� ������.
        retString = readCurse[RandomCurse].GetCurseData();
        curseKey = readCurse[RandomCurse].GetKey();
        readCurse.RemoveAt(RandomCurse);
        return retString;
    }

    public void ClearCurse()
    {
        activeCurse = false;
        curseKey = -1;
        curseText.gameObject.SetActive(false);
        if(activeTimer){
            StopCoroutine(curseTimerCoroutine);
            activeTimer = false;
        }
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
                TimeCurse(120.0f);
            else if(curseKey==19)
                TimeCurse(120.0f);
        }
        /*if(!ending&&die){
            changeCamera.SwitchToVirtualCamera();
            ending=true;
        }*/
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
            Debug.Log("���2");
            GameObject.Find("Alley_close").GetComponent<AlleyNav>().DeadCurse();
            changeCamera.SwitchToVirtualCamera();
            ending=true;
            ClearCurse();
            //StartCoroutine(Death());
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
            Debug.Log("���3");
            GameObject.Find("Alley_close").GetComponent<AlleyNav>().DeadCurse();
            ending=true;
            changeCamera.SwitchToVirtualCamera();
            ClearTimer();
          //  StartCoroutine(Death());

        }
    }



    IEnumerator CurseTimer(float curseTime)
    {
        activeTimer = true;
        float curTime = curseTime;
        while (curTime > 0)
        {
            curTime -= 1;
            countTimer = (int)curTime;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("���4");
        GameObject.Find("Alley_close").GetComponent<AlleyNav>().DeadCurse();
        ending=true;
        changeCamera.SwitchToVirtualCamera();
        //StartCoroutine(Death());
    }
    private void ClearTimer()
    {
        if (activeTimer)
        {
            StopCoroutine(curseTimerCoroutine);
            ClearCurse();
            activeTimer = false;
        }
    }
    /*IEnumerator Death(){
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        LoadingScene.Instance.LoadScene("Death");
    }*/
}
