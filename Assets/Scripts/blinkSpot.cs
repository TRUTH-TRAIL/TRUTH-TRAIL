using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkSpot : MonoBehaviour
{
    [SerializeField] GameObject Alley;
    public Vector3 Alley_pos;
    [SerializeField] GameObject N_Alley;
    // Start is called before the first frame update
    public void C_Blink(){
        StartCoroutine(blinkspot());
    }
    IEnumerator blinkspot(){
        int i = 0;
        while(i < 3){
            i++;
            this.transform.GetComponent<Light>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            this.transform.GetComponent<Light>().enabled = true;
            yield return new WaitForSeconds(0.5f);
            if(i == 1){
                Alley.SetActive(false);
            }
            if(i == 3){
                N_Alley.SetActive(true);
                N_Alley.transform.position = Alley_pos;
            }
        }
    }
}
