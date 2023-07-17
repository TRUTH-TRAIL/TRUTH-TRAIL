using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFSM : MonoBehaviour
{
    public bool isOpen;
    public float openAngle;
    // Start is called before the first frame update
    private void Awake(){
        isOpen = false;
    }

}
