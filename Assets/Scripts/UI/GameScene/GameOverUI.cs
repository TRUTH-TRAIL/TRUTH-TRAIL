using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    Button RetryBtn;
    Button BackToMainBtn;


    void Start()
    {
        Time.timeScale = 0;


        RetryBtn = this.transform.Find("RetryButton").GetComponent<Button>();
        BackToMainBtn = this.transform.Find("BackToMainButton").GetComponent<Button>();

        RetryBtn.onClick.AddListener(() => RetryBtnEvent());
        BackToMainBtn.onClick.AddListener(() => BackToMainBtnEvent());
    }


    void RetryBtnEvent()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(1);
    }


    void BackToMainBtnEvent()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(0);
    }

}
