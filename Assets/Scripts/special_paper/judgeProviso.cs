using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class judgeProviso : MonoBehaviour
{
    public ArrayList getproviso = new ArrayList();
    public string[] proviso;
    public string[] trueproviso;
    public string[] falseproviso;
    public string[] curse;
    public Text[] text;
    int a;
    // Start is called before the first frame update
    void Start()
    {
        a = 0;
    }

    private void OnEnable() {
        if(a < 10){
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
            }
            text[a].text = (string)getproviso[a];
            text[a].gameObject.SetActive(true);
            a++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
