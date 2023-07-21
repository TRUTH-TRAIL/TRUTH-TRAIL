using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialPaper : MonoBehaviour
{
    GameObject special_Paper;
    // Start is called before the first frame update
    void Start()
    {
        special_Paper = GameObject.Find("Canvas").transform.Find("specialPaper").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(special_Paper.activeSelf ==true){
            if(Input.GetKeyDown(KeyCode.Tab)){
                special_Paper.SetActive(false);
            }
        }
        else{
            if(Input.GetKeyDown(KeyCode.Tab)){
                special_Paper.SetActive(true);
            }
        }
    }
}
