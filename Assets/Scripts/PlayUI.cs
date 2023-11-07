using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayUI : MonoBehaviour
{
    public GameObject escB;
    public GameObject BG;
    public TextMeshProUGUI text;
    public string[] tutorialStory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(escB.activeSelf){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else{
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetKey(KeyCode.Escape) && !escB.activeSelf){
            escB.SetActive(true);
            Cursor.visible = true;
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
        BG.SetActive(false);
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
    
    public static void TMPDOText(TextMeshProUGUI text, float duration){
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }

    IEnumerator Typing(string talk){
        text.text = talk;
        TMPDOText(text, 1f);

        yield return new WaitForSeconds(1.5f);
        NextTalk();
    }

    public void NextTalk(){
        //tutorialStory.text = nuint;;l
    }
}
