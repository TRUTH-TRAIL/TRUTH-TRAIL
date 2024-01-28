using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NewAlley : MonoBehaviour
{
    public GameObject Alley;
    public GameObject Alley1;
    public GameObject BGM;
    public float3 pos;
    public void Death(){
        Alley1.GetComponent<AlleyNav>().enabled = false;
        Alley.transform.position = pos;
        StartCoroutine(Time());
       // Time.timeScale = 0;
       // LoadingScene.Instance.LoadScene("Death");
    }
    IEnumerator Time(){
        //Alley1.GetComponent<AlleyNav>().enabled = false;
       // Alley1.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(3.0f);
        //Alley.GetComponent<AlleyNav>().enabled = false;
        //Debug.Log(Alley.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0));
        Alley.SetActive(true);
        BGM.SetActive(true);
        transform.gameObject.SetActive(false);
    }
}
