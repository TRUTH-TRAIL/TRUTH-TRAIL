using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField]
    Texture2D crosshairTexture;
    public Color crosshairColor = new Color(0.085f,0.085f,0.085f,1f);
    public bool showCrosshair = true;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnGUI()
    {
        if(showCrosshair)
        {
            float x = Screen.width / 2;
            float y = Screen.height / 2;
            GUI.color = crosshairColor;
            GUI.DrawTexture(new Rect(x - 2, y - 2, 4, 4), crosshairTexture);
            GUI.color = new Color(0.085f,0.085f,0.085f,1f);
        }
        

    }
    public void ChangeColor(Color newColor) // 색상 변경 메서드
    {
        crosshairColor = newColor;
    }

    public void ToggleCrosshair(bool value)
    {
        showCrosshair = value;
    }
}
