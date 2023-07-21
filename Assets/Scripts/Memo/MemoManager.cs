using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//메모 class
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

//Json 직렬화
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

//Memo 내용 Json파일로 추가
public class MemoManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var memos = new List<Memo>();
        memos.Add(new Memo(1, "first clue"));
        memos.Add(new Memo(2, "second clue"));
        memos.Add(new Memo(3, "third clue"));
        memos.Add(new Memo(4, "fourth clue"));
        memos.Add(new Memo(5, "fifth clue"));
        memos.Add(new Memo(6, "sixth clue"));
        memos.Add(new Memo(7, "seventh clue"));
        memos.Add(new Memo(8, "eighth clue"));
        memos.Add(new Memo(9, "ninth clue"));
        memos.Add(new Memo(10, "tenth clue"));
        memos.Add(new Memo(11, "eleventh clue"));
        memos.Add(new Memo(12, "twelfth clue"));
        memos.Add(new Memo(13, "thirteenth clue"));
        memos.Add(new Memo(14, "fourteenth clue"));
        memos.Add(new Memo(15, "fifteenth clue"));

        string memoStr = JsonUtility.ToJson(new Serialization<Memo> (memos));
        Debug.Log(memoStr);
        List<Memo> retMemos = JsonUtility.FromJson<Serialization<Memo>>(memoStr).ToList();

        string scriptPath = Application.dataPath;
        string filePath = Path.Combine(scriptPath, "memo.json");
        File.WriteAllText(filePath, memoStr);
        Debug.Log("JSON 파일 생성: " + filePath);

    }


}
