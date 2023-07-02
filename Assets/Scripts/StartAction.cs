using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void gameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void setting()
    {
        GameObject.Find("Canvas").transform.Find("Setting").gameObject.SetActive(true);
        GameObject.Find("Start").SetActive(false);
    }
    public void esc()
    {
        GameObject.Find("Setting").SetActive(false);
        GameObject.Find("Canvas").transform.Find("Start").gameObject.SetActive(true);
    }
    public void interAction()
    {
        GameObject.Find("Setting").transform.Find("Action").gameObject.SetActive(true);
        GameObject.Find("Setting").transform.Find("Audio").gameObject.SetActive(false);
        GameObject.Find("Setting").transform.Find("Language").gameObject.SetActive(false);
    }
    public void Audio()
    {
        GameObject.Find("Setting").transform.Find("Action").gameObject.SetActive(false);
        GameObject.Find("Setting").transform.Find("Audio").gameObject.SetActive(true);
        GameObject.Find("Setting").transform.Find("Language").gameObject.SetActive(false);
    }
    public void Language()
    {
        GameObject.Find("Setting").transform.Find("Action").gameObject.SetActive(false);
        GameObject.Find("Setting").transform.Find("Audio").gameObject.SetActive(false);
        GameObject.Find("Setting").transform.Find("Language").gameObject.SetActive(true);
    }
}
