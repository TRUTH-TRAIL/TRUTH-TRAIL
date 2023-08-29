using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool isInventoryOpen = false;
    [SerializeField]
    GameObject inventoryUI;
    public GameObject[] buttons;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            if (isInventoryOpen)
            {
                InventoryBackButton();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                inventoryUI.SetActive(true);
                isInventoryOpen = true;
            }
        }
        if(isInventoryOpen && Input.GetKeyDown(KeyCode.Escape)){
            InventoryBackButton();
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
    }
    public void EquipActiveButton(int i)
    {
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
        buttons[i].SetActive(true);
    }
}
