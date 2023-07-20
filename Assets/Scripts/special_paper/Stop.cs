using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    GameObject specialPaper;
    // Start is called before the first frame update
    void Start()
    {
        specialPaper = GameObject.Find("Canvas").transform.Find("specialPaper").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(specialPaper.activeSelf == true){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1.0f;
        }
    }
}
