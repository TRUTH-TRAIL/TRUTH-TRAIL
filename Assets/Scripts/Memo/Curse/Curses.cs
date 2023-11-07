using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Curses : MonoBehaviour
{
    public bool activeCurse = false;
    public int curseKey = 0;
    //단서 습득 시 저주일 때
    //curse.json파일에서 랜덤으로 저주 읽어오기
    private List<Curse> readCurse;
    private int CurseCount = 0;
    public int doorCurseCount = 0;
    [SerializeField] GameObject player;
    [SerializeField] Text curseText;
    private PlayerCrash playerCrash;

    private void Start()
    {
        playerCrash = player.GetComponent<PlayerCrash>();
    }
    public string ActiveCurse()
    {
        int RandomCurse;
        string retString;
        activeCurse = true;
        readCurse = MemoReader.ReadCurse();
        CurseCount = readCurse.Count;
        RandomCurse = Random.Range(0, CurseCount);
        RandomCurse = 3; //Test용
        retString = readCurse[RandomCurse].GetCurseData();
        curseKey = readCurse[RandomCurse].GetKey();
        readCurse.RemoveAt(RandomCurse);
        return retString;
    }

    public void ClaerCurse()
    {
        activeCurse = false;
        curseKey = 0;
        curseText.gameObject.SetActive(false);
    }
    void Update()
    {
        if(activeCurse)
        {
            switch(curseKey)
            {
                case 1:
                    Curse1();
                    break;
                case 2:
                    Curse2();
                    break;
                case 3:
                    Curse3();
                    break;
                case 4:
                    Curse4();
                    break;
            }
        }
    }
    private void Curse1()
    {
        if (playerCrash.collisionObject == "Ladder_Collider")
        {
            Debug.Log("사망@@@@@");//추후 사망처리 함수 호출
            ClaerCurse();
        }
    }
    private void Curse2()
    {
        //마우스 움직임으로 화면이 180도 회전하면 사망
    }
    private void Curse3()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("사망@@@@@");//추후 사망처리 함수 호출
            ClaerCurse();
        }
    }

    private void Curse4()
    {
        if (doorCurseCount == 2)
        {
            Debug.Log("사망@@@@@");//추후 사망처리 함수 호출
            ClaerCurse();
        }
    }
}
