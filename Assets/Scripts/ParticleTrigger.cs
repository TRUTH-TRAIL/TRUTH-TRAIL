using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ParticleTrigger : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particle;
    [SerializeField]
    NavMeshAgent agent;
    Transform door;
    Vector3 target;
    Vector3 origin;
    public bool check;
    [SerializeField]
    GameObject[] Door;
    [SerializeField]
    GameObject TutoBox;
    public TutoText tutoText;
    public TMP_Text text;
    private void Start() {
        door = GameObject.Find("Bookcase_Door_LOD").transform;
        target = GameObject.Find("Door_target").transform.position;
        origin = GameObject.Find("Bookcase_Door_LOD").transform.position;
        check = false;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !check){
            StartCoroutine(particleStop());
        }
    }
    private void OnTriggerStay(Collider other) {
        if(check){
            TutoBox.SetActive(true);
            text.text = tutoText.text[tutoText.i];
            tutoText.i++;
            int i = 0;
            while(i < 4){
                Door[i].GetComponent<Door>().enabled = true;
                i++;
            }
            particle.gameObject.transform.position = GameObject.Find("p_spot_4").transform.position;
            particle.Play();
            agent.SetDestination(GameObject.Find("p_spot_6").transform.position);
            check = false;
        }
    }
    // 이동 후 3초, 10초 멈춤 구현 안 되어 있음
    // 집안 불 꺼야 함
    IEnumerator particleStop(){
        particle.Stop();
        if(this.name == "p_spot_2"){
            yield return new WaitForSeconds(3.0f);
            particle.Play();
            agent.SetDestination(GameObject.Find("p_spot_3").transform.position);
        }
        else if(this.name == "p_spot_3"){
            yield return new WaitForSeconds(3.0f);
            particle.Play();
            agent.SetDestination(GameObject.Find("p_spot_4").transform.position);
        }
        else if(this.name == "p_spot_4"){
            if(LayerMask.LayerToName(GameObject.Find("Alley_Tuto").layer) != "Ignore Raycast"){
                particle.Play();
                agent.SetDestination(GameObject.Find("p_spot_5").transform.position);
                while(door.position != target){
                    door.position = Vector3.Lerp(door.position, target, Time.deltaTime*5.0f);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        else if(this.name == "p_spot_5")
        {
            if(LayerMask.LayerToName(GameObject.Find("Alley_Tuto").layer) != "Ignore Raycast"){
                while(door.position != origin){
                    door.position = Vector3.Lerp(door.position, origin, Time.deltaTime*5.0f);
                    GameObject.Find("Sealed_Coffin_02").transform.position = Vector3.Lerp(GameObject.Find("Sealed_Coffin_02").transform.position, 
                    GameObject.Find("Sealed_target").transform.position, Time.deltaTime*5.0f);
                    yield return new WaitForSeconds(0.1f);
                }
                particle.Stop();
            }
           //yield return new WaitForSeconds(10.0f);
           // 문 원상태 다시
        }
        else if(this.name == "p_spot_6")
        {
            particle.Stop();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
