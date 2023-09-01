using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject AIobject;
    [SerializeField]
    private Camera cam;
    public bool inCamera;
    Vector3 viewPos;
    [SerializeField]
    private GameObject AIMemo;
    // Start is called before the first frame update
    void Start()
    {
        cam = UnityEngine.Camera.main;
        inCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inCamera){
            viewPos = cam.WorldToViewportPoint(AIobject.transform.position);
            if(viewPos.x <= 0 || viewPos.x >= 1 && viewPos.y <= 0 || viewPos.y >= 1){
                AIobject.SetActive(false);
                AIMemo.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            viewPos = cam.WorldToViewportPoint(AIobject.transform.position);
            if(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1){
                inCamera = true;
            }
        }
    }
}
