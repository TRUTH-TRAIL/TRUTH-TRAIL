using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFSM : MonoBehaviour
{
    public bool isOpen;
    public float openAngle;
    
    private void Awake(){
        isOpen = false;
    }

}
