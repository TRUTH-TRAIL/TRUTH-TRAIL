using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ObjectInteract;
using UnityEngine.UI;

public class ObjectDetector : MonoBehaviour
{
    [System.Serializable]
    public class RaycastEvent : UnityEvent<Transform> {}

    [HideInInspector]
    public RaycastEvent raycastEvent = new RaycastEvent();

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    //Outline �غ���
    Material outline;
    Renderer renderers;
    List<Material> materialList = new List<Material>();
    private Renderer currentRenderer = null;

    [SerializeField]
    private CrossHair crosshair;

    //private GameObject decipherText;
    [SerializeField]
    private Text hitText;
    private GameObject handSpecialPaper;
    [SerializeField]
    private GameObject decal;
    private void Awake(){
        mainCamera = Camera.main;
    }
    private void Start()
    {
        outline = new Material(Shader.Find("Draw/OutlineShader"));
        //decipherText = GameObject.Find("Canvas").transform.GetChild(5).gameObject;
        handSpecialPaper = GameObject.FindWithTag("Player").transform.GetChild(3).gameObject;
    }

    private void Update() {
        Vector3 cameraCenter = new Vector3(mainCamera.pixelWidth * 0.5f, mainCamera.pixelHeight * 0.5f, 0f);
        ray = mainCamera.ScreenPointToRay(cameraCenter);
        
        if (Physics.Raycast(ray, out hit, 5) && (hit.transform.CompareTag("Item") || hit.transform.CompareTag("Door")))
        {
            crosshair.ChangeColor(Color.white);
            ObjectType ot = hit.transform.GetComponent<ObjectTypeController>().objectType;
            if(ot== ObjectType.Candle)
            {
                if(handSpecialPaper.activeSelf&&!decal.activeSelf){
                    hitText.text = "해독하기";
                    hitText.gameObject.SetActive(true);
                    //decipherText.SetActive(true);
                }
            }else if(ot == ObjectType.Clue){
                hitText.text = "줍기";
                hitText.gameObject.SetActive(true);
            }else{
                hitText.gameObject.SetActive(false);
            }
            
            /*
            Renderer hitRenderer = hit.transform.gameObject.GetComponent<Renderer>(); materialList.Clear();
            if (currentRenderer == null || currentRenderer != hitRenderer)
            {
                RestoreMaterials();
                currentRenderer = hitRenderer;
                materialList.Clear();
                materialList.AddRange(currentRenderer.sharedMaterials);
                materialList.Add(outline);
                currentRenderer.materials = materialList.ToArray();
            }
            */
            if (Input.GetMouseButtonDown(0))
            {
                //ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                raycastEvent.Invoke(hit.transform);
            }
        }
        else
        {
            crosshair.ChangeColor(new Color(0.085f,0.085f,0.085f,1f));
            hitText.gameObject.SetActive(false);
            //decipherText.SetActive(false);
            //RestoreMaterials();
        }
        

    }

    private void RestoreMaterials()
    {
        if (currentRenderer != null)
        {
            Material[] currentMaterials = currentRenderer.materials;

            if (currentMaterials.Length > 0 && currentMaterials[currentMaterials.Length - 1].shader == outline.shader)
            {
                Material[] newMaterials = new Material[currentMaterials.Length - 1];
                System.Array.Copy(currentMaterials, newMaterials, newMaterials.Length);
                currentRenderer.materials = newMaterials;
            }

            currentRenderer = null;
        }
    }
}
