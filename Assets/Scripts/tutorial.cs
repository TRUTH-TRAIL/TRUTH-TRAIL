using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public GameObject Panel;
    private RaycastHit hitData;
    public CinemachineVirtualCamera mainCamera;
    public float zoomDuration = 3f; // 줌인하는 데 걸리는 시간
    public float targetFOV = 20f; // 목표 Field of View (FOV)

    private float originalFOV; // 초기 FOV
    private float zoomTimer = 0f; // 줌인에 사용되는 타이머
    public TutoText tutoText;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        originalFOV = mainCamera.m_Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        Vector3 rayDir = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform.forward;
        Debug.DrawRay(rayOrigin, rayDir, Color.red, 0.5f);
        if(Physics.Raycast(rayOrigin, rayDir, out hitData, 0.5f)){
            Debug.Log(hitData.collider.name);
            if(hitData.collider.name == "Alley_Tuto"){
                StartCoroutine(Zoom());
            }
            if(hitData.collider.name == "foldednote"){
                if(Input.GetMouseButtonDown(0)){
                    hitData.collider.gameObject.SetActive(false);
                    tutoText.text[tutoText.i] = "<s>□ 집 안에서 Alley를 봉인할 단서를 찾으세요</s>";
                    text.text = tutoText.text[tutoText.i] + "\n";
                    tutoText.i++;
                    text.text += tutoText.text[tutoText.i];
                }
            }
        }
    }
    // 줌인 시 키보드 입력 정지 구현x
    IEnumerator Zoom(){
        while (zoomTimer < zoomDuration)
        {
            zoomTimer += Time.deltaTime; // 타이머 업데이트

            // 보간하여 FOV 조정
            float t = zoomTimer / zoomDuration;
            mainCamera.m_Lens.FieldOfView = Mathf.Lerp(originalFOV, targetFOV, t);
            yield return new WaitForSeconds(0.1f);
        }
        Panel.SetActive(true);
        mainCamera.m_Lens.FieldOfView = 60f;
        GameObject.Find("Alley_Tuto").layer = LayerMask.NameToLayer("Ignore Raycast");
        GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("p_spot_4").transform.position;
    }
}
       /* if(!spinfo[0].activeSelf){
            spinfo[1].SetActive(false);
        }
        Vector3 rayOrigin = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
        Vector3 rayDir = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform.forward;
        Debug.DrawRay(rayOrigin, rayDir, Color.red, 0.5f);
        if(Physics.Raycast(rayOrigin, rayDir, out hitData, 0.5f)){
//            Debug.Log(hitData.collider.name);
            //Debug.Log(hitData.collider.name);
            if(hitData.collider.name == "old_telephone_lod01" && Input.GetMouseButtonDown(0)){
               // audioSource.Stop();
                if(Key.activeSelf)
                    Key_Text.SetActive(true);
                else
                    Phone_Text.SetActive(true);
                // ?뵦 蹂댁씠?뒪 ?쟻?슜 ?삤瑜?
            }
            if(hitData.collider.name == "Key(Clone)" && Input.GetMouseButtonDown(0) && play){
                hitData.transform.gameObject.SetActive(false);
                Key.SetActive(true);
               // audioSource.Play();
            }
           /* if(hitData.collider.name == "Work_Desk_Box_01_LOD0" && Input.GetMouseButtonDown(0) && play){
                Debug.Log(hitData.collider.name);
                hitData.transform.position += new Vector3(1.5f, 0, 0);
            }*/
        /*if(Panel.activeSelf || Phone_Text.activeSelf || Key_Text.activeSelf){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(!Panel.activeSelf && !Phone_Text.activeSelf && !Key_Text.activeSelf){
            time += Time.deltaTime;
            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
        if(time >= 1 && !play){
           // audioSource.Play();
            play = true;
        }*/
    //}
