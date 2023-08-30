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

    //Outline ÇØº¸ÀÚ
    Material outline;
    Renderer renderers;
    List<Material> materialList = new List<Material>();
    private Renderer currentRenderer = null;


    private void Awake(){
        mainCamera = Camera.main;
    }
    private void Start()
    {
        outline = new Material(Shader.Find("Draw/OutlineShader"));
    }

    private void Update() {
        Vector3 cameraCenter = new Vector3(mainCamera.pixelWidth * 0.5f, mainCamera.pixelHeight * 0.5f, 0f);
        ray = mainCamera.ScreenPointToRay(cameraCenter);
        
        if (Physics.Raycast(ray, out hit, 5) && (hit.transform.CompareTag("Item") || hit.transform.CompareTag("Door")))
        {
            
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

            if (Input.GetMouseButtonDown(0))
            {
                //ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                raycastEvent.Invoke(hit.transform);
            }
        }
        else
        {
            RestoreMaterials();
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
