using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItemData
{
    public int uid; // 아이템의 테이블을 참조하기 위한 고유 ID 의미
    public int amount; // 겹치는 카운트
}
[System.Serializable]
public class Inventory
{
    private int maxSlotCount = 18;
    public int MAXSLOTCOUNT { get => maxSlotCount; }
    private int curSlotCount;
    public int CURSLOTCOUNT { get => curSlotCount;
        set => curSlotCount = value;
    }

    [SerializeField]
    private List<InventoryItemData> items = new List<InventoryItemData>();


    public void AddItem(InventoryItemData newItem)
    {
        int index = FindItemIndex(newItem);
        Debug.Log("신규 아이템 추가 " + newItem.uid);
        GameManager.Inst.GetItemData(newItem.uid, out TableItem tableItem);
        
        if(-1 < index && !tableItem.equip)
        {
            items[index].amount += newItem.amount;
        }
        else
        {
            items.Add(newItem);
            curSlotCount++;
        }
    }

    public bool IsFull()
    {
        return curSlotCount >= maxSlotCount;
    }

    private int FindItemIndex(InventoryItemData newItem)
    {
        int result = -1;

        for(int i = items.Count - 1; i >= 0; i--)
        {
            if(items[i].uid == newItem.uid)
            {
                result = i;
                break;
            }
        }
        return result;
    }

    public List<InventoryItemData> GetItemList()
    {
        return items;
    }


    public void DeleteItem(InventoryItemData deleteItem)
    {
        int index = FindItemIndex(deleteItem);
        if(index > -1)
        {
            items[index].amount -= deleteItem.amount;
            if(items[index].amount < 1)
            {
                items.RemoveAt(index);
                curSlotCount--;
            }
        }
    }

}
