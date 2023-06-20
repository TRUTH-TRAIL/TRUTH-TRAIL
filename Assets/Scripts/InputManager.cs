using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    

    private float _deltaMouseX;
    private float _deltaMouseY;
    // Start is called before the first frame update
    public void OnUpdate()
    {
        SetDeltaMousePos();
    }

    private void SetDeltaMousePos()
    {
        _deltaMouseX = Input.GetAxisRaw("Mouse X");
        _deltaMouseX = Input.GetAxisRaw("Mouse Y");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
