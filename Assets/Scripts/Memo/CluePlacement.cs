using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePlacement : MonoBehaviour
{
    [SerializeField]
    GameObject cluePrefab;  //�ܼ� prefab
    private List<GameObject> clues = new List<GameObject>();  //������ �ܼ� list

    private int totalLocations = 15;  //�ܼ��� ����� ��� ��
    private int clueListCount = 15;  //json�� �� �ִ� ������ ��

    private int totalClues = 10;  //real + fake + curse
    private int realClue = 3;
    private int fakeClue = 5;
    private int cursedClue = 2;

    private List<Vector3> cluePositions = new List<Vector3>();

    //int�� �� ������ clue list�� 15�� �ְ�, 10���� real, fake ������ �ȴٸ� 0�� ������, 1�� real, 2�� fake
    private int[] clueType = new int[15]; // �� �κ��� ���� GameManager�� ����Ǿ� ����

    private List<Memo> readMemos;
    // Start is called before the first frame update
    void Start()
    {
        readMemos = MemoReader.ReadMemos();
        //���Ƿ� ������ ��ġ(�� �κ��� ���� ����)
        cluePositions.Add(new Vector3(300, 0, 221.2534f));
        cluePositions.Add(new Vector3(302, 0, 221.2534f));
        cluePositions.Add(new Vector3(304, 0, 221.2534f));
        cluePositions.Add(new Vector3(306, 0, 221.2534f));
        cluePositions.Add(new Vector3(308, 0, 221.2534f));
        cluePositions.Add(new Vector3(310, 0, 221.2534f));
        cluePositions.Add(new Vector3(312, 0, 221.2534f));
        cluePositions.Add(new Vector3(314, 0, 221.2534f));
        cluePositions.Add(new Vector3(316, 0, 221.2534f));
        cluePositions.Add(new Vector3(318, 0, 221.2534f));
        cluePositions.Add(new Vector3(320, 0, 221.2534f));
        cluePositions.Add(new Vector3(322, 0, 221.2534f));
        cluePositions.Add(new Vector3(324, 0, 221.2534f));
        cluePositions.Add(new Vector3(326, 0, 221.2534f));
        cluePositions.Add(new Vector3(328, 0, 221.2534f));

        PlaceClues();

    }

    private void PlaceClues()
    {
        List<int> selectedPositions = new List<int>();

        while (selectedPositions.Count < totalClues)
        {
            int randomIndex = Random.Range(0, cluePositions.Count);
            if (!selectedPositions.Contains(randomIndex))  //�ܼ��� ������ ��ġ�� �����Ǵ� ���� ����
            {
                selectedPositions.Add(randomIndex);
                GameObject newClue = Instantiate(cluePrefab, cluePositions[randomIndex], Quaternion.identity);  //������ ��ġ�� �ܼ� ����
                clues.Add(newClue);
            }
        }
        ClueType();
    }

    private void ClueType()
    {

        for(int i = 0; i < totalClues; i++)
        {
            if (i < realClue)
            {
                int key = RandomClue(1);
                clues[i].GetComponent<MemoScript>().key = key;
                clues[i].GetComponent<MemoScript>().type = true;
                clues[i].GetComponent<MemoScript>().memoData = readMemos[key].GetMemoData();
                
            }
            else if (i>= (realClue + fakeClue))
            {
                //�� �κ��� ���ְ� ��(�ӽ�)
                clues[i].GetComponent<MemoScript>().memoData = "����";
            }
            else
            {
                int key = RandomClue(2);
                clues[i].GetComponent<MemoScript>().key = key;
                clues[i].GetComponent<MemoScript>().type = false;
                clues[i].GetComponent<MemoScript>().memoData = readMemos[key].GetMemoData();
            }
        }
    }

    private int RandomClue(int type)
    {
        int randomIndex;
        //���� �ߺ��� ���� ��ȿ���������� �ܼ��� �����Ƿ� �ϴ� �̷��� �ۼ��غ�
        do
        {
            randomIndex = Random.Range(0, clueListCount);
        }
        while (clueType[randomIndex] != 0);

        clueType[randomIndex] = type;
        return randomIndex;

    }

}
