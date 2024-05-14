using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
//¾È¾¸
public class CameraViewCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject AIobject;
    [SerializeField]
    private GameObject player;
  //  [SerializeField]
   // private Camera cam;
    public bool inCamera;
   // Vector3 viewPos;
    //[SerializeField]
    //private GameObject AIMemo;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Image Fade_Panel;
    [SerializeField]
    private GameObject postProcessing;
    float fadeCount = 0;
    [SerializeField]
    private GameObject Alley_Text;
    [SerializeField]
    private GameObject[] obj;
    //public Text text;
    // Start is called before the first frame update
    void Start()
    {
     //   cam = UnityEngine.Camera.main;
        inCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
       // Vector3 dis = new Vector3(284.549988f,3.9000001f,267.524994f) - 
        //new Vector3(100f,60f,0);
        //text.rectTransform.position = GameObject.Find("poster_S").transform.position + dis;
        /*if(inCamera){
            viewPos = cam.WorldToViewportPoint(AIobject.transform.position);
            if(viewPos.x <= 0 || viewPos.x >= 1 && viewPos.y <= 0 || viewPos.y >= 1){
                AIobject.SetActive(false);
                AIMemo.SetActive(true);
            }
        }*/
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && !inCamera){
            inCamera = true;
            anim.SetTrigger("Hit");
            StartCoroutine(delay());
            /*viewPos = cam.WorldToViewportPoint(AIobject.transform.position);
            if(viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1){
                inCamera = true;
            }*/
        }
    }

    IEnumerator delay(){
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("Idle");
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut(){
        while(fadeCount < 1.0f){ //&& player.transform.rotation.z <= 90.0f){
            fadeCount += 0.01f;
            //player.transform.Rotate(new Vector3(0, 0, -0.9f));
            yield return new WaitForSeconds(0.01f);
            Fade_Panel.color = new Color(0, 0, 0, fadeCount);
        }
        StartCoroutine(FadeIn());

    }
    IEnumerator FadeIn(){
       // float objectTimeScale = 0f;
       // float customDeltaTime = objectTimeScale * Time.deltaTime;
       // player.GetComponent<PlayerController>().
        AIobject.SetActive(false);
        postProcessing.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        Debug.Log("fadein");
        while(fadeCount > 0){ // && player.transform.rotation.z >= 0){
            fadeCount -= 0.005f;
            //player.transform.Rotate(new Vector3(0, 0, 0.9f));
            yield return new WaitForSeconds(0.01f);
            Fade_Panel.color = new Color(0, 0, 0, fadeCount);
        }
        Alley_Text.SetActive(true);
        obj[0].SetActive(true);
        obj[1].SetActive(true);
    }
}
