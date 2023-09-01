using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectInteract;

public class CluePlacement : MonoBehaviour
{
    [SerializeField]
    GameObject cluePrefab;  //�ܼ� prefab
    private List<GameObject> clues = new List<GameObject>();  //������ �ܼ� list

    private int totalLocations = 15;  //�ܼ��� ����� ��� ��
    private int clueListCount = 15;  //json�� �� �ִ� ������ ��

    private int totalClues = 10;  //real + fake + curse
    private int realClue = 5;
    private int fakeClue = 0;
    private int cursedClue = 5;

    private List<Vector3> cluePositions = new List<Vector3>();

    //int�� �� ������ clue list�� 15�� �ְ�, 10���� real, fake ������ �ȴٸ� 0�� ������, 1�� real, 2�� fake
    private int[] clueType = new int[15]; // �� �κ��� ���� GameManager�� ����Ǿ� ����

    private List<Memo> readMemos;
    // Start is called before the first frame update
    void Start()
    {
        readMemos = MemoReader.ReadMemos();
        //���Ƿ� ������ ��ġ(�� �κ��� ���� ����)
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
                AttachClueToNearbyObject(newClue);
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
