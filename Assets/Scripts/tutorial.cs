using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject[] text;
    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            text[1].SetActive(true);
            StartCoroutine(start());
        }
    }

    public void skip(){
        Panel.SetActive(false);
    }

    public IEnumerator start(){
        yield return new WaitForSeconds(1.0f);
        if(text[1].activeSelf == true){
            Panel.SetActive(false);
        }
    }
}
