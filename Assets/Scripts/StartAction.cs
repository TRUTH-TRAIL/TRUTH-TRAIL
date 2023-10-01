using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
        LoadingScene.Instance.LoadScene("tutorial");
    }
    public void StartB()
    {
        Start.SetActive(true);
        title.SetActive(false);
    }
    public void OptionB()
    {
        Option.SetActive(true);
        title.SetActive(false);
    }
    public void Back()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        title.SetActive(true);
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
