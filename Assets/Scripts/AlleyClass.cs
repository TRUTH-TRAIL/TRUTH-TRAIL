using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyClass
{
    private static AlleyClass alleyClass;
    private int p;
    public static AlleyClass getalleyClass
    {
        get
        {
            if(alleyClass == null){
                alleyClass = new AlleyClass();
            }
            return alleyClass;
        }
    }

    private AlleyClass()
    {
        
    }
    public void SpotNum(int s){
        Debug.Log(s);
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
        //spotn = SpotMove(s, p);
    }

 //   public int SpotMove(){

   // }
    
    public void SMove(){

    }
}
