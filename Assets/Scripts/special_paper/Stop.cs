using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    GameObject specialPaper;
    GameObject player;
    public float PlayertimeScale;
    // Start is called before the first frame update
    void Start()
    {
        specialPaper = GameObject.Find("Canvas").transform.Find("specialPaper").gameObject;
        player = GameObject.Find("Player");
        PlayertimeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(specialPaper.activeSelf == true){
            PlayertimeScale = 0f;
        }
        else{
            PlayertimeScale = 1.0f;
        }
    }
}
