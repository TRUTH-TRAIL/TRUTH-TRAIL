using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePlacement : MonoBehaviour
{
    [SerializeField]
    GameObject cluePrefab;  //단서 prefab
    private List<GameObject> clues = new List<GameObject>();  //생성된 단서 list

    private int totalLocations = 15;  //단서가 생기는 장소 수
    private int totalClues = 10;
    private int realClue = 3;
    private int fakeClue = 5;
    private int cursedClue = 2;

    private List<Vector3> cluePositions = new List<Vector3>();




    // Start is called before the first frame update
    void Start()
    {
        //임의로 선정된 위치(이 부분은 추후 수정)
        cluePositions.Add(new Vector3(1, 0, 0));
        cluePositions.Add(new Vector3(3, 0, 0));
        cluePositions.Add(new Vector3(5, 0, 0));
        cluePositions.Add(new Vector3(7, 0, 0));
        cluePositions.Add(new Vector3(9, 0, 0));
        cluePositions.Add(new Vector3(11, 0, 0));
        cluePositions.Add(new Vector3(13, 0, 0));
        cluePositions.Add(new Vector3(15, 0, 0));
        cluePositions.Add(new Vector3(17, 0, 0));
        cluePositions.Add(new Vector3(19, 0, 0));
        cluePositions.Add(new Vector3(21, 0, 0));
        cluePositions.Add(new Vector3(23, 0, 0));
        cluePositions.Add(new Vector3(25, 0, 0));
        cluePositions.Add(new Vector3(27, 0, 0));
        cluePositions.Add(new Vector3(29, 0, 0));

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
                //clues[i].GetComponent<>().type = "realClue"   이 부분은 clue script 작성 후 추후 수정
                //clues[i].GetComponent<>().text = ""   이 부분은 clue text list에서 랜덤 추출하여 추가(근데 이 부분이 어디있지?)
            }
            else if (i>= (realClue + fakeClue))
            {
                //clues[i].GetComponent<>().type = "curseClue"   이 부분은 clue script 작성 후 추후 수정
            }
            else
            {
                //clues[i].GetComponent<>().type = "fakeClue"   이 부분은 clue script 작성 후 추후 수정
            }
        }
    }

}
