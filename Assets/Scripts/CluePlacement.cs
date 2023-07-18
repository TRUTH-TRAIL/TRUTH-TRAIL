using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePlacement : MonoBehaviour
{
    [SerializeField]
    GameObject cluePrefab;  //�ܼ� prefab
    private List<GameObject> clues = new List<GameObject>();  //������ �ܼ� list

    private int totalLocations = 15;  //�ܼ��� ����� ��� ��
    private int totalClues = 10;
    private int realClue = 3;
    private int fakeClue = 5;
    private int cursedClue = 2;

    private List<Vector3> cluePositions = new List<Vector3>();




    // Start is called before the first frame update
    void Start()
    {
        //���Ƿ� ������ ��ġ(�� �κ��� ���� ����)
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
                //clues[i].GetComponent<>().type = "realClue"   �� �κ��� clue script �ۼ� �� ���� ����
                //clues[i].GetComponent<>().text = ""   �� �κ��� clue text list���� ���� �����Ͽ� �߰�(�ٵ� �� �κ��� �������?)
            }
            else if (i>= (realClue + fakeClue))
            {
                //clues[i].GetComponent<>().type = "curseClue"   �� �κ��� clue script �ۼ� �� ���� ����
            }
            else
            {
                //clues[i].GetComponent<>().type = "fakeClue"   �� �κ��� clue script �ۼ� �� ���� ����
            }
        }
    }

}
