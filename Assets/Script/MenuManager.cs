using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryObj;
    private bool inventoryIsOpen;
    private InventoryUI inventoryUI;


    private void Awake()
    {
        inventoryUI = inventoryObj.GetComponent<InventoryUI>();
        InitMenuManager();
    }

    public void InitMenuManager()
    {
        inventoryObj.LeanScale(Vector3.zero, 0.01f);
        inventoryIsOpen = false;
    }

    public void ShowInventory()
    {
        inventoryIsOpen = !inventoryIsOpen;

        if (inventoryIsOpen)
        {
            inventoryUI.RefreshIcon();
            inventoryObj.LeanScale(Vector3.one, 0.7f).setEase(LeanTweenType.easeInOutElastic);
        }
        else
            inventoryObj.LeanScale(Vector3.zero, 0.7f).setEase(LeanTweenType.easeInOutElastic);
    }
}
