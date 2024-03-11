using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    Transform door;
    CameraViewCheck cameraViewCheck;
    public GameObject Key;
   // public GameObject tuText;
    private void Start() {
        //door = GameObject.Find("Interior_Door_01 (3)").transform.GetChild(0).gameObject.transform;
        cameraViewCheck = GameObject.Find("AICube").GetComponent<CameraViewCheck>();
       // tuText = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
    }
    private void OnTriggerEnter(Collider other) {
        //if(tuText.activeSelf == true){
          //  Debug.Log("?");
        //    tuText.SetActive(false);
       // }
        if(other.CompareTag("Player")){
            if(Key.activeSelf){
               // GameObject.Find("Interior_Door_01 (3)").transform.GetChild(0).GetComponent<Door>().enabled = true;
            }
            if(cameraViewCheck.inCamera == true)
            {
                LoadingScene.Instance.LoadScene("GameScene_woo");
               // door.GetComponent<Door>().OpenDoor(other.transform);
            }
        }
    }

    IEnumerator gameStart(){
        Time.timeScale = 0;
        yield return new WaitForSeconds(2.0f);
        LoadingScene.Instance.LoadScene("GameScene_woo");
    }
}