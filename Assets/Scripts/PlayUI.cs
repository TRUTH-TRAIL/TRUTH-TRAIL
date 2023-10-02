using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.SceneManagement;

public class PlayUI : MonoBehaviour
{
    public GameObject escB;
    public GameObject BG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(escB.activeSelf == false){
            if(Input.GetKey(KeyCode.Escape)){
                escB.SetActive(true);
                Cursor.visible = true;
            }
        }
        else{
            if(Input.GetKey(KeyCode.Escape)){
                escB.SetActive(false);
                Cursor.visible = false;
            }
        }
    }
    public void Continue(){
        Debug.Log("?");
        escB.SetActive(false);
    }
    public void Main(){
        LoadingScene.Instance.LoadScene("Start");
    }

    public void Exitcheck(){
        BG.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
