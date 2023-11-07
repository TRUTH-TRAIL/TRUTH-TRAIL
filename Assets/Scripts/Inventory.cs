using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool isInventoryOpen = false;
    [SerializeField]
    GameObject inventoryUI;
    public GameObject[] buttons;
    [SerializeField]
    private CrossHair crosshair;
    [SerializeField]
    private GameObject specialPaper;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)&&!specialPaper.activeSelf){
            if (isInventoryOpen)
            {
                InventoryBackButton();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                crosshair.ToggleCrosshair(false);
                inventoryUI.SetActive(true);
                isInventoryOpen = true;
            }
        }
        if(isInventoryOpen && Input.GetKeyDown(KeyCode.Escape)){
            InventoryBackButton();
        }
        if (specialPaper.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(0))
            {
                specialPaper.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                crosshair.ToggleCrosshair(true);
            } 
        }
    }
    public void InventoryBackButton()
    {
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inventoryUI.SetActive(false);
        isInventoryOpen = false;
        crosshair.ToggleCrosshair(true);
    }
    public void EquipActiveButton(int i)
    {
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
        buttons[i].SetActive(true);
    }
    public void OpenSpecialPaper()
    {
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
        specialPaper.SetActive(true);
        inventoryUI.SetActive(false);
        isInventoryOpen = false;
    }
}
