using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ray : MonoBehaviour
{
    RaycastHit hitData;
    public GameObject[] poster;
    public GameObject[] postertx;
    GameObject tuText;
    bool check;
    GameObject sp;
    // Start is called before the first frame update
    void Start()
    {
        sp = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        poster = new GameObject[3];
        poster[0] = GameObject.Find("Canvas").transform.Find("Inventory").GetChild(4).transform.GetChild(0).gameObject;
        poster[1] = GameObject.Find("Canvas").transform.Find("Inventory").GetChild(4).transform.GetChild(1).gameObject;
        poster[2] = GameObject.Find("Canvas").transform.Find("Inventory").GetChild(4).transform.GetChild(2).gameObject;
        
        postertx = new GameObject[3];
        postertx[0] = GameObject.Find("Canvas").transform.GetChild(1).transform.GetChild(1).gameObject;
        postertx[1] = GameObject.Find("Canvas").transform.GetChild(1).transform.GetChild(2).gameObject;
        postertx[2] = GameObject.Find("Canvas").transform.GetChild(1).transform.GetChild(3).gameObject;
        tuText = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 1f, Color.red, 0.3f);
        if(Input.GetMouseButtonDown(0)){
            if(Physics.Raycast(transform.position, transform.forward, out hitData, 1f)){
                hitData.collider.gameObject.SetActive(false);
                if(hitData.collider.name == "poster"){
                    poster[0].SetActive(true);
                }
                else if(hitData.collider.name == "poster (1)"){
                    poster[1].SetActive(true);
                }
                else if(hitData.collider.name == "poster (2)"){
                    poster[2].SetActive(true);
                }
            }
        }

        if(poster[0].activeSelf == true && poster[1].activeSelf == true && poster[2].activeSelf == true){
            if(tuText.activeSelf == false && check == false)
                tuText.SetActive(true);
                check = true;
            StartCoroutine(textDestroy());
        }

        if(sp.activeSelf == true){
            if(Input.GetKeyDown(KeyCode.Escape)){
                sp.SetActive(false);
            }
        }
    }

    public void posterClick(Button button){
        GameObject.Find("Inventory").SetActive(false);
        sp.SetActive(true);
        if(button.name == "Button"){
            postertx[0].SetActive(true);
            postertx[1].SetActive(false);
            postertx[2].SetActive(false);
        }
        else if(button.name == "Button (1)"){
            postertx[0].SetActive(false);
            postertx[1].SetActive(true);
            postertx[2].SetActive(false);
        }
        else if(button.name == "Button (2)"){
            postertx[0].SetActive(false);
            postertx[1].SetActive(false);
            postertx[2].SetActive(true);
        }
    }

    IEnumerator textDestroy()
    {
        yield return new WaitForSeconds(5.0f);
        tuText.SetActive(false);
    }
}
