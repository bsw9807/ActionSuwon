using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;

    private List<InventorySlot> slotList = new List<InventorySlot>();
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private RectTransform contentTrans;
    private void Awake()
    {
        inventory = GameManager.Inst.Inventory;
        InitSlots();
    }

    InventorySlot slot;
    void InitSlots()
    {
        for(int i = 0; i < inventory.MAXSLOTCOUNT; i++)
        {
            slot = Instantiate(slotPrefab, contentTrans).GetComponent<InventorySlot>();
            slotList.Add(slot);
        }
    }

    List<InventoryItemData> dataList;

    public void RefreshIcon()
    {
        inventory = GameManager.Inst.Inventory;
        dataList = inventory.GetItemList();
        inventory.CURSLOTCOUNT = dataList.Count;

        for(int i = 0; i < inventory.MAXSLOTCOUNT; i++)
        {
            if (i < inventory.CURSLOTCOUNT && -1 < dataList[i].uid)
            {
                slotList[i].DrawItem(dataList[i]);
            }
            else
                slotList[i].ClearSlot();
        }
    }

}
