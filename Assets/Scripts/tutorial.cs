using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject Panel;
    private float time;
    public AudioSource audioSource;
    private bool play;
    public GameObject Phone_Text;
    public GameObject Key_Text;
    private RaycastHit hitData;
    public GameObject Key;

    // Start is called before the first frame update
    void Start()
    {
        play = false;
        audioSource = GameObject.Find("old_telephone_lod01").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        Vector3 rayDir = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform.forward;
        Debug.DrawRay(rayOrigin, rayDir, Color.red, 0.5f);
        if(Physics.Raycast(rayOrigin, rayDir, out hitData, 0.5f)){
            //Debug.Log(hitData.collider.name);
            if(hitData.collider.name == "old_telephone_lod01" && Input.GetMouseButtonDown(0)){
                audioSource.Stop();
                if(Key.activeSelf)
                    Key_Text.SetActive(true);
                else
                    Phone_Text.SetActive(true);
                // 딥 보이스 적용 오류
            }
            if(hitData.collider.name == "Key(Clone) (1)" && Input.GetMouseButtonDown(0) && play){
                hitData.transform.gameObject.SetActive(false);
                Key.SetActive(true);
                audioSource.Play();
            }
        }
        if(Panel.activeSelf || Phone_Text.activeSelf || Key_Text.activeSelf){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(!Panel.activeSelf && !Phone_Text.activeSelf && !Key_Text.activeSelf){
            time += Time.deltaTime;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(time >= 1 && !play){
            audioSource.Play();
            play = true;
        }
    }
}
