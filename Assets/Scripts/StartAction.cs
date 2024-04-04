using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartAction : MonoBehaviour
{
    public GameObject title;
    public GameObject Start;
    public GameObject Option;
   // public GameObject Back;
    public GameObject BG;
    public GameObject[] Option_list;
    //public GameObject VideoPanel;
    //public GameObject ControlPanel;

    // Start is called before the first frame update
    public void GameStart()
    {
        LoadingScene.Instance.LoadScene("0");
    }
    public void StartB()
    {
        Start.transform.GetChild(0).GetComponent<Text>().enabled = true;
        Start.transform.GetChild(1).GetComponent<Text>().enabled = true;
        Start.transform.GetChild(2).GetComponent<Text>().enabled = true;
        //Start.SetActive(true);
        title.transform.GetChild(0).GetComponent<Text>().enabled = false;
        title.transform.GetChild(1).GetComponent<Text>().enabled = false;
        title.transform.GetChild(2).GetComponent<Text>().enabled = false;
    }
    public void OptionB()
    {
        Option.transform.GetChild(1).GetComponent<Text>().enabled = true;
        Option.transform.GetChild(2).GetComponent<Text>().enabled = true;
        Option.transform.GetChild(3).GetComponent<Text>().enabled = true;
        Option.transform.GetChild(4).GetComponent<Text>().enabled = true;
        Option.transform.GetChild(5).GetComponent<Text>().enabled = true;
       // Option.SetActive(true);
        title.transform.GetChild(0).GetComponent<Text>().enabled = false;
        title.transform.GetChild(1).GetComponent<Text>().enabled = false;
        title.transform.GetChild(2).GetComponent<Text>().enabled = false;
    }
    public void Back()
    {
        if(EventSystem.current.currentSelectedGameObject.transform.name == "S_Back"){
            EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(0).GetComponent<Text>().enabled = false;
            EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(1).GetComponent<Text>().enabled = false;
            EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(2).GetComponent<Text>().enabled = false;
        }
        else{
            //EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(0).GetComponent<Text>().enabled = false;
            EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(1).GetComponent<Text>().enabled = false;
            EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(2).GetComponent<Text>().enabled = false;
            EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(3).GetComponent<Text>().enabled = false;
            EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(4).GetComponent<Text>().enabled = false;
            EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(5).GetComponent<Text>().enabled = false;
        }
        title.transform.GetChild(0).GetComponent<Text>().enabled = true;
        title.transform.GetChild(1).GetComponent<Text>().enabled = true;
        title.transform.GetChild(2).GetComponent<Text>().enabled = true;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Video()
    {
        Option_list[1].transform.GetChild(0).gameObject.SetActive(false);
        Option_list[2].transform.GetChild(0).gameObject.SetActive(false);
        Option_list[3].transform.GetChild(0).gameObject.SetActive(false);
        if(Option_list[0].transform.GetChild(0).gameObject.activeSelf == true){
            BG.SetActive(false);
            //VideoPanel.SetActive(false);
            Option_list[0].transform.GetChild(0).gameObject.SetActive(false);
        }
        else{
            BG.SetActive(true);
           // VideoPanel.SetActive(true);
            Option_list[0].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void Audio()
    {
        Option_list[0].transform.GetChild(0).gameObject.SetActive(false);
        Option_list[2].transform.GetChild(0).gameObject.SetActive(false);
        Option_list[3].transform.GetChild(0).gameObject.SetActive(false);
        if(Option_list[1].transform.GetChild(0).gameObject.activeSelf == true){
            BG.SetActive(false);
            Option_list[1].transform.GetChild(0).gameObject.SetActive(false);
        }
        else{
            BG.SetActive(true);
            Option_list[1].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void Control()
    {
        Option_list[0].transform.GetChild(0).gameObject.SetActive(false);
        Option_list[1].transform.GetChild(0).gameObject.SetActive(false);
        Option_list[3].transform.GetChild(0).gameObject.SetActive(false);
        if(Option_list[2].transform.GetChild(0).gameObject.activeSelf == true){
            BG.SetActive(false);
            Option_list[2].transform.GetChild(0).gameObject.SetActive(false);
            //ControlPanel.SetActive(false);
        }
        else{
            BG.SetActive(true);
           // ControlPanel.SetActive(true);
            Option_list[2].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void Language(){
        Option_list[0].transform.GetChild(0).gameObject.SetActive(false);
        Option_list[1].transform.GetChild(0).gameObject.SetActive(false);
        Option_list[2].transform.GetChild(0).gameObject.SetActive(false);
        if(Option_list[3].transform.GetChild(0).gameObject.activeSelf == true){
            BG.SetActive(false);
            Option_list[3].transform.GetChild(0).gameObject.SetActive(false);
            //ControlPanel.SetActive(false);
        }
        else{
            BG.SetActive(true);
           // ControlPanel.SetActive(true);
            Option_list[3].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
