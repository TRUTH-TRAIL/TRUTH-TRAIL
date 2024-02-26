using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlleyTrigger : MonoBehaviour
{
    AlleySpot alleySpot;
    bool start_check;
    private void Start() {
        start_check = false;
        StartCoroutine(StartChange());
        alleySpot = GameObject.Find("spot").transform.GetComponent<AlleySpot>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "AI" && start_check){
            if(alleySpot.i == alleySpot.str.Length){
                alleySpot.i = 0;
                alleySpot.SpotNum(alleySpot.spotn);
            }
            alleySpot.alleyNav.agent.SetDestination(GameObject.Find(alleySpot.str[alleySpot.i]).transform.position);
            if(alleySpot.str[alleySpot.i] == "3_spot_3"){
                StartCoroutine(alleySpot.Standstill());
            }
            else if(alleySpot.str[alleySpot.i] == "5_spot_4"){
                if(alleySpot.p == 1){
                    StartCoroutine(alleySpot.Standstill());
                }
            }
            else if(alleySpot.str[alleySpot.i] == "0_spot"){
                if(alleySpot.p == 1){
                    StartCoroutine(alleySpot.Standstill());
                }
            }
            alleySpot.i++;
            alleySpot.alleyNav.state = AlleyNav.State.Idle;
        }
    }

    IEnumerator StartChange(){
        yield return new WaitForSeconds(1.0f);
        start_check = true;
    }
}
