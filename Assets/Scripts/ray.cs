using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{
    RaycastHit hitData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 15f, Color.red, 0.3f);
        if(Input.GetMouseButtonDown(0)){
            if(Physics.Raycast(transform.position, transform.forward, out hitData, 15f)){
                Debug.Log(hitData.collider.name);
                hitData.collider.gameObject.SetActive(false);
            }
        }
    }
}
