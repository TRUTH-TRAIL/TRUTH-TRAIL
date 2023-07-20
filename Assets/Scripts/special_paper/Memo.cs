using UnityEngine;

[System.Serializable]
public class Memo
{
    public bool memoType;
    public string memoData;
    bool[] type;
    string[] memoList;
    public void setmemoList()
    {
        type = new bool[2] {true, false};
        memoList = new string[8] {"단1", "단2", "단3", "단1", "단2", "단3", "단4", "단5"};
    }
    public Memo()
    {
        setmemoList();
        memoType = type[Random.Range(0, 2)];
        memoData = memoList[Random.Range(0, 8)];
    }
}
