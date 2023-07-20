using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class judgeProviso : MonoBehaviour
{
    public List<Memo> getproviso = new List<Memo>();
   /* public string[] proviso;
    public bool[] trueproviso;
    public bool[] falseproviso;
    public string[] curse;*/
    public Text[] text;
    public string memoType;
    public string memoData;
    int index = 0;
   /* void Awake()
    {
        for(int i = 0; i < 10; i++){
            text[i] = GameObject.Find("Canvas").transform.Find("specialPaper").transform.Find("list").transform.GetChild(i).gameObject.GetComponent<Text>();
        }
    }*/
    void setMemo(){
        Memo memo = new Memo();
        string jsonData = JsonUtility.ToJson(memo);
        memoType = jsonData.Substring(jsonData.IndexOf("memoType") + 10, jsonData.IndexOf(",") - jsonData.IndexOf(":") - 1);
        memoData = jsonData.Substring(jsonData.IndexOf("memoData") + 11, jsonData.IndexOf("}") - jsonData.IndexOf("memoData") - 12);
    }

    private void OnEnable() {
        if(index < 10){
            setMemo();
            getproviso.Add(new Memo() { memoType = Convert.ToBoolean(memoType), memoData = memoData});
            foreach(Memo memo in getproviso){
                text[index].text = memo.memoData;
            }
            text[index].gameObject.SetActive(true);
            index++;
        }
    }
}
        /*if(a < 10){
            bool find = false;
            int r = Random.Range(0, 9);
            getproviso.Add(proviso[r]);
            while(find == false){
                for(int i = 0; i < 6; i++){
                    if(getproviso[a].Equals(trueproviso[i])){
                        find = true;
                    }
                }
                for(int i = 0; i < 4; i++){
                    if(getproviso[a].Equals(falseproviso[i])){
                        find = true;
                    }
                }
                for(int i = 0; i < 3; i++){
                    if(getproviso[a].Equals(curse[i])){
                        text[a].color = Color.red;
                        find = true;
                    }
                }
            }*/

