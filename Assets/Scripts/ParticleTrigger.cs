using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem particle;
    public NavMeshAgent agent;
    Transform door;
    Vector3 target;
    [SerializeField]
    Vector3 origin;
    public bool check;
    [SerializeField]
    GameObject[] Door;
    [SerializeField]
    GameObject TutoBox;
    [SerializeField]
    TutoText tutoText;
    [SerializeField]
    TMP_Text text;
    private void Start() {
        tutoText = GameObject.Find("Canvas").transform.Find("Tuto_TextBox").transform.Find("Text (TMP)").GetComponent<TutoText>();
        text = GameObject.Find("Canvas").transform.Find("Tuto_TextBox").transform.Find("Text (TMP)").GetComponent<TMP_Text>();
        door = GameObject.Find("Bookcase_Door_LOD").transform;
        target = GameObject.Find("Door_target").transform.position;
        origin = GameObject.Find("Door_origin").transform.position;
        check = false;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && !check){
            StartCoroutine(particleStop());
        }
    }
    private void OnTriggerStay(Collider other) {
        if(check){
            particle.gameObject.SetActive(false);
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
           // agent.SetDestination(GameObject.Find("p_spot_6").transform.position);
            check = false;
        }
    }
    // �̵� �� 3��, 10�� ���� ���� �� �Ǿ� ����
    // ���� �� ���� ��
    IEnumerator particleStop(){
        particle.Stop();
        if(this.name == "p_spot_2"){
            yield return new WaitForSeconds(3.0f);
            particle.Play();
            agent.SetDestination(GameObject.Find("p_spot_3").transform.position);
        }
        else if(this.name == "p_spot_3"){
            if(LayerMask.LayerToName(GameObject.Find("Alley_Tuto").layer) != "Ignore Raycast"){
                yield return new WaitForSeconds(3.0f);
                particle.Play();
                agent.SetDestination(GameObject.Find("p_spot_4").transform.position);
            }
        }
        else if(this.name == "p_spot_4"){
            if(LayerMask.LayerToName(GameObject.Find("Alley_Tuto").layer) != "Ignore Raycast"){
                particle.Play();
                agent.SetDestination(GameObject.Find("p_spot_5").transform.position);
                while(door.position != target){
                    door.position = Vector3.Lerp(door.position, target, Time.deltaTime*5.0f);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        else if(this.name == "p_spot_5"){
            if(LayerMask.LayerToName(GameObject.Find("Alley_Tuto").layer) != "Ignore Raycast"){
                GameObject.Find("P_spot").transform.GetChild(6).gameObject.SetActive(true);
                yield return null;
            }
        }
        else if(this.name == "p_spot_7")
        {
            if(LayerMask.LayerToName(GameObject.Find("Alley_Tuto").layer) != "Ignore Raycast"){
                while(door.position != origin){ // �� ���峪�ϱ� �̰� �ƴ� �Ÿ� ��ʷ�...?
                    door.position = Vector3.Lerp(door.position, origin, Time.deltaTime*10.0f);
                    GameObject.Find("Sealed_Coffin_02").transform.position = Vector3.Lerp(GameObject.Find("Sealed_Coffin_02").transform.position, 
                    GameObject.Find("Sealed_target").transform.position, Time.deltaTime*5.0f);
                    yield return new WaitForSeconds(0.01f);
                }
                // �� ������ �� �̻���... �ٽ� �� �� ���� �ڵ�
              //  GameObject.Find("P_spot").transform.GetChild(6).gameObject.SetActive(false);
               // particle.Stop();
            }
           //yield return new WaitForSeconds(10.0f);
           // ���Ͻǹ��̰��� �� ������ �ٽ�
        }
        /*else if(this.name == "p_spot_6")
        {
           // particle.Stop();
            yield return new WaitForSeconds(1.0f);
        }*/
    }
}
