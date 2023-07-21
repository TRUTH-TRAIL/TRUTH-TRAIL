using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePlacement : MonoBehaviour
{
    [SerializeField]
    GameObject cluePrefab;  //단서 prefab
    private List<GameObject> clues = new List<GameObject>();  //생성된 단서 list

    private int totalLocations = 15;  //단서가 생기는 장소 수
    private int clueListCount = 15;  //json에 들어가 있는 데이터 수

    private int totalClues = 10;  //real + fake + curse
    private int realClue = 3;
    private int fakeClue = 5;
    private int cursedClue = 2;

    private List<Vector3> cluePositions = new List<Vector3>();

    //int로 한 이유는 clue list가 15개 있고, 10개만 real, fake 지정이 된다면 0은 미지정, 1은 real, 2는 fake
    private int[] clueType = new int[15]; // 이 부분은 추후 GameManager에 저장되어 관리

    private List<Memo> readMemos;
    // Start is called before the first frame update
    void Start()
    {
        readMemos = MemoReader.ReadMemos();
        //임의로 선정된 위치(이 부분은 추후 수정)
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
            if (!selectedPositions.Contains(randomIndex))  //단서가 동일한 위치에 생성되는 것을 방지
            {
                selectedPositions.Add(randomIndex);
                GameObject newClue = Instantiate(cluePrefab, cluePositions[randomIndex], Quaternion.identity);  //랜덤한 위치에 단서 생성
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
                //이 부분은 저주가 들어감(임시)
                clues[i].GetComponent<MemoScript>().memoData = "저주";
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
        //뭔가 중복이 많아 비효율적이지만 단서가 적으므로 일단 이렇게 작성해봄
        do
        {
            randomIndex = Random.Range(0, clueListCount);
        }
        while (clueType[randomIndex] != 0);

        clueType[randomIndex] = type;
        return randomIndex;

    }

}
