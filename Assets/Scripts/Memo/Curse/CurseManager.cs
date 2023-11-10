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
    void Start()
    {
        var curse = new List<Curse>();
        curse.Add(new Curse(0, "계단을 오르내리면 넌 죽어"));
        curse.Add(new Curse(1, "한번에 뒤를 돌아보면 넌 죽어"));
        curse.Add(new Curse(2, "옆으로 움직이면 넌 죽어"));
        curse.Add(new Curse(3, "방 문을 두번 열면 넌 죽어"));

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
