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
    [SerializeField]
    private GameObject handSpecialPaper;
    public GameObject handSkull;
    public GameObject handCross;
    public GameObject handCandle;
    public GameObject handLighter;
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
        if(handSpecialPaper.activeSelf||handSkull.activeSelf||handCross.activeSelf||handCandle.activeSelf||handLighter.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.Tab)){
                handSpecialPaper.SetActive(false);
                handSkull.SetActive(false);
                handCross.SetActive(false);
                handCandle.SetActive(false);
                handLighter.SetActive(false);
            }
            
        }
        if(Input.GetKeyDown(KeyCode.R)){
            if(specialPaper.activeSelf){
                specialPaper.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                crosshair.ToggleCrosshair(true);
            }else{
                crosshair.ToggleCrosshair(false);
                OpenSpecialPaper();
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
        if(i==5)
            buttons[6].SetActive(true);
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
    public void EquipItem(int i)
    {
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
        switch(i){
            case 0:
                handSpecialPaper.SetActive(true);
                break;
            case 1:
                handSkull.SetActive(true);
                break;
            case 2:
                handCross.SetActive(true);
                break;
            case 3:
                handCandle.SetActive(true);
                break;
            case 4:
                handLighter.SetActive(true);
                break;
        }
        
        bool a = isInventoryOpen;
        InventoryBackButton();
        isInventoryOpen = a;
    }


}
