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
    //�ܼ� ���� �� ������ ��
    //curse.json���Ͽ��� �������� ���� �о����
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
        RandomCurse = 3; //Test��
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
            Debug.Log("���@@@@@");//���� ���ó�� �Լ� ȣ��
            ClaerCurse();
        }
    }
    private void Curse2()
    {
        //���콺 ���������� ȭ���� 180�� ȸ���ϸ� ���
    }
    private void Curse3()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("���@@@@@");//���� ���ó�� �Լ� ȣ��
            ClaerCurse();
        }
    }

    private void Curse4()
    {
        if (doorCurseCount == 2)
        {
            Debug.Log("���@@@@@");//���� ���ó�� �Լ� ȣ��
            ClaerCurse();
        }
    }
}
