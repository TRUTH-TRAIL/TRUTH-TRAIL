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
        alleySpot = GameObject.Find("spot").transform.GetComponent<AlleySpot>();
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log(this.name);
        if(other.tag == "AI"){
            //Debug.Log(this.name);
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
            if(alleySpot.i == alleySpot.str.Length){
                alleySpot.i = 0;
                alleySpot.SpotNum(alleySpot.spotn);
            }
            alleySpot.alleyNav.agent.SetDestination(GameObject.Find(alleySpot.str[alleySpot.i]).transform.position);
            alleySpot.alleyNav.state = AlleyNav.State.Idle;
        }
    }
}
