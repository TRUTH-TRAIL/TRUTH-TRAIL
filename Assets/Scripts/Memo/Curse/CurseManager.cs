using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//�޸� class
[Serializable]
public class Curse
{
    public int key;
    public string curseData;

    public Curse(int key, string memoData)
    {
        this.key = key;
        this.curseData = memoData;
    }
    public int GetKey()
    {
        return key;
    }

    public string GetCurseData()
    {
        return curseData;
    }
}



//Curse ���� Json���Ϸ� �߰�
public class CurseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var curse = new List<Curse>();
        curse.Add(new Curse(0, "����� ���������� �� �׾�"));
        curse.Add(new Curse(1, "�ѹ��� �ڸ� ���ƺ��� �� �׾�"));
        curse.Add(new Curse(2, "������ �����̸� �� �׾�"));
        curse.Add(new Curse(3, "�� ���� �ι� ���� �� �׾�"));

        curse.Add(new Curse(10, "������ �Ѻ� 2:00"));
        curse.Add(new Curse(11, "�߼Ҹ��� ���� 3:30"));
        curse.Add(new Curse(12, "õõ�� �ɾ�� 1:00"));
        curse.Add(new Curse(13, "�ٸ� ������ �Һ��� ����� 5:00"));
        curse.Add(new Curse(14, "���� ����� 1:00"));
        curse.Add(new Curse(15, "������� ���� 2:30"));
        curse.Add(new Curse(16, "���ڸ� ������ 3:00"));
        curse.Add(new Curse(17, "������ ����� 3:00"));
        curse.Add(new Curse(18, "å�� ������ 2:00"));
        curse.Add(new Curse(19, "õ���� �ٶ�� 2:00"));

        string curseStr = JsonUtility.ToJson(new Serialization<Curse>(curse));
        Debug.Log(curseStr);
        List<Curse> retCurse = JsonUtility.FromJson<Serialization<Curse>>(curseStr).ToList();

        string scriptPath = Application.dataPath;
        string filePath = Path.Combine(scriptPath, "curse.json");
        File.WriteAllText(filePath, curseStr);
        Debug.Log("JSON ���� ����: " + filePath);

    }


}
