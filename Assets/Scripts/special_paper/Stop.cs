using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Canvas").transform.Find("specialPaper").gameObject.activeSelf == true){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1.0f;
        }
    }
}
