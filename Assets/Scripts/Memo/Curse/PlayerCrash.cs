using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrash : MonoBehaviour
{
    public string collisionObject;
    [SerializeField] Curses curse;
    private void OnCollisionEnter(Collision collision)
    {
        if (curse.activeCurse)
        {
            if(curse.curseKey == 0||curse.curseKey==15)
            {
                if (collision.gameObject.name == "Ladder_Collider")
                {
                    Debug.Log("OnCollisionEnter " + collision.gameObject.name);
                    curse.die = true;
                }
            }
            
        }
        
    }
}
