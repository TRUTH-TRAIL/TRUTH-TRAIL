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
        curse.Add(new Curse(1, "계단을 오르내리면 넌 죽어"));
        curse.Add(new Curse(2, "한번에 뒤를 돌아보면 넌 죽어"));
        curse.Add(new Curse(3, "옆으로 움직이면 넌 죽어"));
        curse.Add(new Curse(4, "방 문을 두번 열면 넌 죽어"));

        curse.Add(new Curse(5, "손전등 불빛을 사용하면 안돼 2:00"));
        curse.Add(new Curse(6, "발 소리를 내면 안돼 3:30"));


        string curseStr = JsonUtility.ToJson(new Serialization<Curse>(curse));
        Debug.Log(curseStr);
        List<Curse> retCurse = JsonUtility.FromJson<Serialization<Curse>>(curseStr).ToList();

        string scriptPath = Application.dataPath;
        string filePath = Path.Combine(scriptPath, "curse.json");
        File.WriteAllText(filePath, curseStr);
        Debug.Log("JSON 파일 생성: " + filePath);

    }


}
