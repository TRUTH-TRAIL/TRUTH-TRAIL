using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//�޸� class
[Serializable]
public class Memo
{
    public int key;
    public string memoData;

    public Memo(int key, string memoData)
    {
        this.key = key;
        this.memoData = memoData;
    }
    public int GetKey()
    {
        return key;
    }

    public string GetMemoData()
    {
        return memoData;
    }
}

//Json ����ȭ
public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }
    public Serialization(List<T> target)
    {
        this.target = target;
    }
}

//Memo ���� Json���Ϸ� �߰�
public class MemoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var memos = new List<Memo>();
        memos.Add(new Memo(0, "���� 4�ð� �Ǳ� ���� ������ ������ ���ľ� �Ѵ�."));
        memos.Add(new Memo(1, "�����ʹ� OO�� �ִ�."));
        memos.Add(new Memo(2, "�ذ��� OO�� �ִ�."));
        memos.Add(new Memo(3, "���ڰ��� OO�� �ִ�."));
        memos.Add(new Memo(4, "�� ���� ���� 3���� ��� ��ƶ�."));
        memos.Add(new Memo(5, "�𸶽��� ������ �������� ã�ƶ�."));
        memos.Add(new Memo(6, "3���� ���ʸ� ������ �������� ���ƶ�."));
        memos.Add(new Memo(7, "�������� ����� �ذ��� ���ƶ�."));
        memos.Add(new Memo(8, "�ذ� �տ� ���ڰ��� ���ƶ�."));
        memos.Add(new Memo(9, "�ذ� �𸶼��� ���̰� ���ʿ� ���� �ٿ���."));
        //memos.Add(new Memo(11, "eleventh clue"));
        //memos.Add(new Memo(12, "twelfth clue"));
        //memos.Add(new Memo(13, "thirteenth clue"));
        //memos.Add(new Memo(14, "fourteenth clue"));
        //memos.Add(new Memo(15, "fifteenth clue"));

        string memoStr = JsonUtility.ToJson(new Serialization<Memo> (memos));
        Debug.Log(memoStr);
        List<Memo> retMemos = JsonUtility.FromJson<Serialization<Memo>>(memoStr).ToList();

        string scriptPath = Application.dataPath;
        string filePath = Path.Combine(scriptPath, "memo.json");
        File.WriteAllText(filePath, memoStr);
        Debug.Log("JSON ���� ����: " + filePath);

    }


}
