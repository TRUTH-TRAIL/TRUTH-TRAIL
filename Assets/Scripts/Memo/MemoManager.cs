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
    void Awake()
    {
        var memos = new List<Memo>();
        memos.Add(new Memo(0, "지하실 입구를 가로막고 있는 책장은 맨 아래칸의 오른쪽 책을 뺴면 열린다"));
        memos.Add(new Memo(1, "라이터는 부엌 안쪽 창고의 쇼파 아래에 있다."));
        memos.Add(new Memo(2, "지하실 관을 열어보면 앨리의 머리쪽 유골이 있을 것이다."));
        memos.Add(new Memo(3, "욕조의 물을 내리면 십자가가 있을 것이다."));
        memos.Add(new Memo(4, "집 안의 양초 3개를 모두 모아라."));
        memos.Add(new Memo(5, "퇴마식을 진행할 마법진을 찾아라."));
        memos.Add(new Memo(6, "3개의 양초를 마법진 꼭짓점에 놓아라."));
        memos.Add(new Memo(7, "마법진의 가운데에 해골을 놓아라."));
        memos.Add(new Memo(8, "해골 앞에 십자가를 놓아라."));
        memos.Add(new Memo(9, "해골에 퇴마서를 붙이고 양초에 불을 붙여라."));
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
        Debug.Log("JSON 파일 생성: " + filePath);

    }


}
