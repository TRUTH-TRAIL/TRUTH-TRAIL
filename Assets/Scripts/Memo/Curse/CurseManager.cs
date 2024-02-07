using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


//메모 class
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



//Curse 내용 Json파일로 추가
public class CurseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var curse = new List<Curse>();
        curse.Add(new Curse(0, "계단으로 와볼래?"));
        curse.Add(new Curse(1, "뒤돌아봐"));
        curse.Add(new Curse(2, "옆으로 움직여 볼래?"));
        curse.Add(new Curse(3, "방 문을 두번 열어봐"));
        curse.Add(new Curse(4, "지하실 문을 열어봐")); //미구현
        curse.Add(new Curse(5, "창문 밖을 봐"));
        curse.Add(new Curse(6, "서랍을 열어봐"));
        curse.Add(new Curse(7, "책장의 책을 치워봐"));
        curse.Add(new Curse(8, "너 앞으로 되게 잘 걷는다. 계속 앞으로 걸어봐"));
        curse.Add(new Curse(9, "화장실에 뭔가 있어. 확인해볼래?"));

        curse.Add(new Curse(10, "손전등 켜봐 2:00"));
        curse.Add(new Curse(11, "발소리를 내봐 3:30"));
        curse.Add(new Curse(12, "천천히 걸어봐 1:00"));
        curse.Add(new Curse(13, "다른 손전등 불빛을 비춰봐 5:00"));
        curse.Add(new Curse(14, "문을 열어봐 1:00"));
        curse.Add(new Curse(15, "계단으로 가봐 2:30"));
        curse.Add(new Curse(16, "액자를 만져봐 3:00"));
        curse.Add(new Curse(17, "서랍을 열어봐 3:00"));
        curse.Add(new Curse(18, "책을 만져봐 2:00"));
        curse.Add(new Curse(19, "천장을 바라봐 2:00"));

        string curseStr = JsonUtility.ToJson(new Serialization<Curse>(curse));
        Debug.Log(curseStr);
        List<Curse> retCurse = JsonUtility.FromJson<Serialization<Curse>>(curseStr).ToList();

        string scriptPath = Application.dataPath;
        string filePath = Path.Combine(scriptPath, "curse.json");
        File.WriteAllText(filePath, curseStr);
        Debug.Log("JSON 파일 생성: " + filePath);

    }


}
