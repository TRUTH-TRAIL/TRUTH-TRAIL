using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlleySpot : MonoBehaviour
{
    public AlleyNav alleyNav;
    //[SerializeField] NavMeshAgent agent;
    public string[] str;
    public int i;
    public int p;
    public int spotn;
    private void Start() {
        alleyNav = GameObject.Find("Alley_close").GetComponent<AlleyNav>();
       // agent = GameObject.Find("Alley_close").GetComponent<NavMeshAgent>();
    }
    public void SpotNum(int s)
    {
        switch(s){
            case 0:
                p = Random.Range(0, 2);
                break;
            case 1:
                p = Random.Range(0, 3);
                break;
            case 2:
                p = Random.Range(0, 1);
                break;
            case 3:
                p = Random.Range(0, 4);
                break;
            case 4:
                p = Random.Range(0, 1);
                break;
            case 5:
                p = Random.Range(0, 1);
                break;
            case 6:
                p = 0;
                break;
            case 7:
                p = Random.Range(0, 1);
                break;
            case 8:
                p = Random.Range(0, 2);
                break;
            default:
                break;
        }
        spotn = SpotMove(s, p);
    }
    public int SpotMove(int s, int p){
        int spotNumber = 0;
        switch(s){
            case 0:
                switch(p){
                    case 0:
                        str = new string[3]{"0_spot_1", "8_spot", "4_spot"};
                        spotNumber = 4;
                        break;
                    case 1:
                        str = new string[1]{"0_spot"};
                        spotNumber = 0;
                        break;
                }
                break;
            case 1:
                switch(p){
                    case 0:
                        str = new string[1]{"6_spot"};
                        spotNumber = 6;
                        break;
                    case 1:
                        str = new string[1]{"3_spot"};
                        spotNumber = 3;
                        break;
                    case 2:
                        str = new string[1]{"0_spot"};
                        spotNumber = 0;
                        break;
                }
                break;
            case 2:
                switch(p){
                    case 0:
                        str = new string[1]{"1_spot"};
                        spotNumber = 1;
                        break;
                    case 1:
                        str = new string[2]{"8_spot", "4_spot"};
                        spotNumber = 4;
                        break;
                }
                break;
            case 3:
                switch(p){
                    case 0:
                        str = new string[2]{"3_spot_1", "8_spot"};
                        spotNumber = 8;
                        break;
                    case 1:
                        str = new string[2]{"3_spot_2", "8_spot"};
                        spotNumber = 8;
                        break;
                    case 2:
                        str = new string[1]{"2_spot"};
                        spotNumber = 2;
                        break;
                    case 3:
                        str = new string[1]{"3_spot_3"};
                        spotNumber = 3;
                        break;
                }
                break;
            case 4:
                switch(p){
                    case 0:
                        str = new string[4]{"7_spot", "4_spot_1", "4_spot_2", "7_spot"};
                        spotNumber = 7;
                        break;
                    case 1:
                        str = new string[3]{"3_spot_2", "4_spot_3", "6_spot"};
                        spotNumber = 6;
                        break;
                }
                break;
            case 5:
                switch(p){
                    case 0:
                        str = new string[7]{"5_spot_1", "5_spot_2", "6_spot", "5_spot_3", "4_spot_3", "5_spot_4", "6_spot"};
                        spotNumber = 6;
                        break;
                    case 1:
                        str = new string[4]{"5_spot", "5_spot_4", "5_spot", "1_spot"};
                        spotNumber = 1;
                        break;
                    }
                break;
            case 6:
                switch(p){
                    case 0:
                        str = new string[2]{"5_spot", "1_spot"};
                        spotNumber = 1;
                        break;
                    case 1:
                        str = new string[3]{"5_spot_3", "4_spot_3", "7_spot"};
                        spotNumber = 7;
                        break;
                    case 2:
                        str = new string[1]{"4_spot"};
                        spotNumber = 4;
                        break;
                }
                break;
            case 7:
                switch(p){
                    case 0:
                        str = new string[7]{"4_spot_2", "4_spot_1", "6_spot", "5_spot_3", "4_spot_3", "5_spot_4", "6_spot"};
                        spotNumber = 6;
                        break;
                    case 1:
                    // 10초간 �?????만히x
                        str = new string[3]{"7_spot_1", "5_spot_3", "6_spot"};
                        spotNumber = 6;
                        break;
                }
                break;
            case 8:
                switch(p){
                    case 0:
                        str = new string[1]{"4_spot"};
                        spotNumber = 4;
                        break;
                    case 1:
                        str = new string[1]{"1_spot"};
                        spotNumber = 1;
                        break;
                    case 2:
                        str = new string[1]{"2_spot"};
                        spotNumber = 2;
                        break;
                }
                break;
            default:
                break;
        }
        alleyNav.state = AlleyNav.State.Walk;
        return spotNumber;
    }
    public void SMove(string[] s){
        if(i == s.Length){
            i = 0;
            SpotNum(spotn);
        }
        alleyNav.agent.SetDestination(GameObject.Find(s[i]).transform.position);
        /*if(!agent.pathPending){
            if(agent.remainingDistance <= agent.stoppingDistance){
                if(!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if(s[i] == "3_spot_3"){
                        StartCoroutine(Standstill());
                    }
                    else if(s[i] == "5_spot_4"){
                        if(p == 1){
                            StartCoroutine(Standstill());
                        }
                    }
                    else if(s[i] == "0_spot"){
                        if(p == 1){
                            StartCoroutine(Standstill());
                        }
                    }
                    i++;
                    alleyNav.state = AlleyNav.State.Idle;
                } 
            }
        }*/
    }
    public IEnumerator Standstill(){
        alleyNav.agent.speed = 0;
        alleyNav.state = AlleyNav.State.Idle;
        yield return new WaitForSeconds(7.0f);
        alleyNav.state = AlleyNav.State.Walk;
    }
}
