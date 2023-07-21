using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDetector : MonoBehaviour
{
    [System.Serializable]
    public class RaycastEvent : UnityEvent<Transform> {}

    [HideInInspector]
    public RaycastEvent raycastEvent = new RaycastEvent();

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private void Awake(){
        mainCamera = Camera.main;
    }
    private void Update(){
        if(Input.GetMouseButtonDown(0)){
            Vector3 cameraCenter = new Vector3(mainCamera.pixelWidth * 0.5f, mainCamera.pixelHeight * 0.5f, 0f);
            ray = mainCamera.ScreenPointToRay(cameraCenter);
            //ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit,5)){
                raycastEvent.Invoke(hit.transform);
            }
        }
    }
}
