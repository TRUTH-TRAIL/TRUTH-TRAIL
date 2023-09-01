using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ray : MonoBehaviour
{
    RaycastHit hitData;
    public GameObject[] poster;
    GameObject tuText;
    bool check;
    // Start is called before the first frame update
    void Start()
    {
        poster = new GameObject[3];
        poster[0] = GameObject.Find("Canvas").transform.Find("Inventory").GetChild(4).transform.GetChild(0).gameObject;
        poster[1] = GameObject.Find("Canvas").transform.Find("Inventory").GetChild(4).transform.GetChild(1).gameObject;
        poster[2] = GameObject.Find("Canvas").transform.Find("Inventory").GetChild(4).transform.GetChild(2).gameObject;
        tuText = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 15f, Color.red, 0.3f);
        if(Input.GetMouseButtonDown(0)){
            if(Physics.Raycast(transform.position, transform.forward, out hitData, 15f)){
                Debug.Log(hitData.collider.name);
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
    }

    IEnumerator textDestroy()
    {
        yield return new WaitForSeconds(5.0f);
        tuText.SetActive(false);
    }
}
