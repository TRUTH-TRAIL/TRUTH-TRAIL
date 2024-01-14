using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NewAlley : MonoBehaviour
{
    public GameObject Alley;
    public GameObject BGM;
    public float3 pos;
    public void Death(){
        Alley.transform.position = pos;
        StartCoroutine(Time());
       // Time.timeScale = 0;
       // LoadingScene.Instance.LoadScene("Death");
    }
    IEnumerator Time(){
        yield return new WaitForSeconds(2.0f);
        Alley.SetActive(true);
        BGM.SetActive(true);
        transform.gameObject.SetActive(false);
    }
}
