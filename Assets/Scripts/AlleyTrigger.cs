using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlleyTrigger : MonoBehaviour
{
    AlleySpot alleySpot;
    private void Start() {
        alleySpot = GameObject.Find("spot").transform.GetComponent<AlleySpot>();
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("?");
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
