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
    // Start is called before the first frame update
    void Start()
    {
        play = false;
        audioSource = GameObject.Find("old_telephone_lod01").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if(Input.GetKeyDown(KeyCode.O)){
            audioSource.Stop();
            Phone_Text.SetActive(true);
            // 딥 보이스 적용 오류
        }
        if(Input.GetKeyDown(KeyCode.K)){
            audioSource.Stop();
            Key_Text.SetActive(true);
            // 딥 보이스 적용 오류
        }
       /* if(GameObject.Find("Player").GetComponent<Player>().hitData.collider.name == "old_telephone_lod01"
        && Input.GetMouseButtonDown(0)){
            audioSource.Stop(); // 레이 인식 안 됨
        }
        */
    }
}
