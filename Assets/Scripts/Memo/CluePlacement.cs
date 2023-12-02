using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectInteract;

public class CluePlacement : MonoBehaviour
{
    [SerializeField]
    GameObject cluePrefab;  //단서 prefab
    private List<GameObject> clues = new List<GameObject>();  //생성된 단서 list

    private int totalLocations = 15;  //단서가 생기는 장소 수
    private int clueListCount = 10;  //json에 들어가 있는 데이터 수

    private int totalClues = 10;  //real + fake + curse
    private int realClue = 10;
    private int fakeClue = 0;
    private int cursedClue = 0;

    private List<Vector3> cluePositions = new List<Vector3>();

    //int로 한 이유는 clue list가 15개 있고, 10개만 real, fake 지정이 된다면 0은 미지정, 1은 real, 2는 fake
    private int[] clueType = new int[15]; // 이 부분은 추후 GameManager에 저장되어 관리

    private List<Memo> readMemos;
    // Start is called before the first frame update
    private List<int> selectedPositions = new List<int>();
    void Start()
    {
        readMemos = MemoReader.ReadMemos();
        //임의로 선정된 위치(이 부분은 추후 수정)
        cluePositions.Add(new Vector3(271.053894f, 7.32079983f, 253.649994f));
        cluePositions.Add(new Vector3(271.56601f, 6.65299988f, 258.602997f));
        cluePositions.Add(new Vector3(272.390015f, 7.10300016f, 258.571991f));
        cluePositions.Add(new Vector3(276.166168f, 6.94990015f, 257.136475f));
        cluePositions.Add(new Vector3(276.928986f, 6.27600002f, 253.695999f));
        cluePositions.Add(new Vector3(275.903992f, 6.8130002f, 256.533997f));
        cluePositions.Add(new Vector3(276.497986f, 7.61100006f, 257.723999f));
        cluePositions.Add(new Vector3(266.422211f, 6.51200008f, 249.523224f));
        cluePositions.Add(new Vector3(269.98999f, 6.76999998f, 248.823959f));
        cluePositions.Add(new Vector3(269.492004f, 6.63800001f, 252.872955f));
        cluePositions.Add(new Vector3(267.501007f, 7.18100023f, 253.358597f));
        cluePositions.Add(new Vector3(266.760834f, 6.99319983f, 258.552185f));
        cluePositions.Add(new Vector3(276.5f, 7.75400019f, 257.723999f));
        cluePositions.Add(new Vector3(268.214752f, 6.8670001f, 266.479004f));
        cluePositions.Add(new Vector3(274.506989f, 6.61000013f, 267.640991f));
        cluePositions.Add(new Vector3(270.282318f,7.08282232f,275.717285f));

        cluePositions.Add(new Vector3(276.060089f,11.2821751f,265.143555f));
        cluePositions.Add(new Vector3(277.403625f,10.5632601f,251.702194f));
        cluePositions.Add(new Vector3(279.341095f,10.8466787f,246.56105f));
        cluePositions.Add(new Vector3(270.18399f,10.9901247f,245.886002f));
        cluePositions.Add(new Vector3(257.691986f,10.9280005f,247.705002f));
        cluePositions.Add(new Vector3(252.616592f,10.8380003f,266.587006f));
        PlaceClues();

    }

    private void PlaceClues()
    {

        while (selectedPositions.Count < totalClues)
        {
            int randomIndex = Random.Range(0, cluePositions.Count);
            if (!selectedPositions.Contains(randomIndex))  //단서가 동일한 위치에 생성되는 것을 방지
            {
                selectedPositions.Add(randomIndex);
                GameObject newClue = Instantiate(cluePrefab, cluePositions[randomIndex], Quaternion.identity);  //랜덤한 위치에 단서 생성
                AttachClueToNearbyObject(newClue);
                clues.Add(newClue);
            }
        }
        ClueType();
    }
    public void RelocationClue(GameObject clue){
        int leftClue = totalLocations - selectedPositions.Count;
        int randomIndex = Random.Range(0,leftClue);
        int cnt = -1;
        int locClue;
        // 선정되지 않은 위치 중 randomIndex번째 위치 추출
        for(locClue=0; locClue<totalLocations; locClue++){
            if(!selectedPositions.Contains(locClue)){
                cnt++;
            }
            if(cnt==randomIndex)
                break;
        }
        selectedPositions.Add(locClue);

        clue.transform.position = cluePositions[locClue];
        AttachClueToNearbyObject(clue);
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

    private void AttachClueToNearbyObject(GameObject clue)
    {
        float searchRadius = 0.1f;
        Collider[] hitColliders = Physics.OverlapSphere(clue.transform.position, searchRadius);

        foreach (var hitCollider in hitColliders)
        {
            ObjectTypeController objTypeController = hitCollider.GetComponent<ObjectTypeController>();
            if (objTypeController && objTypeController.objectType == ObjectType.Drawer)
            {
                clue.transform.SetParent(hitCollider.transform, true);
                break;
            }
            if (objTypeController && objTypeController.objectType == ObjectType.Frame)
            {
                clue.transform.Rotate(0f, 0f, 90f);
                clue.transform.SetParent(hitCollider.transform, true);
                break;
            }
        }
    }
}
