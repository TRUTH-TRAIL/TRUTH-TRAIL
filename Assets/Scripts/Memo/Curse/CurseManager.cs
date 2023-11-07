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
        curse.Add(new Curse(1, "����� ���������� �� �׾�"));
        curse.Add(new Curse(2, "�ѹ��� �ڸ� ���ƺ��� �� �׾�"));
        curse.Add(new Curse(3, "������ �����̸� �� �׾�"));
        curse.Add(new Curse(4, "�� ���� �ι� ���� �� �׾�"));

        curse.Add(new Curse(5, "������ �Һ��� ����ϸ� �ȵ� 2:00"));
        curse.Add(new Curse(6, "�� �Ҹ��� ���� �ȵ� 3:30"));


        string curseStr = JsonUtility.ToJson(new Serialization<Curse>(curse));
        Debug.Log(curseStr);
        List<Curse> retCurse = JsonUtility.FromJson<Serialization<Curse>>(curseStr).ToList();

        string scriptPath = Application.dataPath;
        string filePath = Path.Combine(scriptPath, "curse.json");
        File.WriteAllText(filePath, curseStr);
        Debug.Log("JSON ���� ����: " + filePath);

    }


}
