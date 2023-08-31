using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    Transform door;
    CameraViewCheck cameraViewCheck;
    private void Start() {
        door = GameObject.Find("Interior_Door_01 (3)").transform.GetChild(0).gameObject.transform;
        cameraViewCheck = GameObject.Find("AICube").GetComponent<CameraViewCheck>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            if(cameraViewCheck.inCamera == true)
                door.GetComponent<Door>().OpenDoor(other.transform);
        }
    }
}