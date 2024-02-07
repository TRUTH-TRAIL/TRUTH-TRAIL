using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MemoReader : MonoBehaviour
{
    // json���Ϸ� ���� �޸� ���� �б�
    public static List<Memo> ReadMemos()
    {
        string filePath = Path.Combine(Application.dataPath, "memo.json");

        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                List<Memo> retMemos = JsonUtility.FromJson<Serialization<Memo>>(json).ToList();
                return retMemos;
            }
            catch (System.Exception e)
            {
                Debug.LogError("JSON ���� �б� ����: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("JSON ������ �������� �ʽ��ϴ�: " + filePath);
        }
        return new List<Memo>();
    }

    public static List<Curse> ReadCurse()
    {
        string filePath = Path.Combine(Application.dataPath, "curse.json");
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                List<Curse> retCurse = JsonUtility.FromJson<Serialization<Curse>>(json).ToList();
                return retCurse;
            }
            catch (System.Exception e)
            {
                Debug.LogError("JSON ���� �б� ����: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("JSON ������ �������� �ʽ��ϴ�: " + filePath);
        }
        
        return new List<Curse>();
    }

}
