using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialPaper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Canvas").transform.Find("specialPaper").gameObject.activeSelf ==true){
            if(Input.GetKeyDown(KeyCode.Tab)){
                GameObject.Find("Canvas").transform.Find("specialPaper").gameObject.SetActive(false);
                Time.timeScale = 0;
            }
        }
        else{
            if(Input.GetKeyDown(KeyCode.Tab)){
                GameObject.Find("Canvas").transform.Find("specialPaper").gameObject.SetActive(true);
                Time.timeScale = 1.0f;
            }
        }
    }
}
