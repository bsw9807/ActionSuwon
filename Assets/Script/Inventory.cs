using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData
{
    public int uid; // 아이템의 테이블을 참조하기 위한 고유 ID 의미
    public int amount; // 겹치는 카운트
}
public class Inventory
{
    private List<InventoryItemData> items = new List<InventoryItemData>();


    public void AddItem(InventoryItemData newItem)
    {
        int index = FindItemIndex(newItem);
        if(-1 < index)
        {
            items[index].amount += newItem.amount;
        }
        else
        {
            items.Add(newItem);
        }


        Debug.Log("================================");
        for(int i = 0; i < items.Count; i++)
        {
            Debug.Log("아이템 리스트 " + i + "  " + items[i].uid + "   " + items[i].amount);
        }
        Debug.Log("================================");
    }

    private int FindItemIndex(InventoryItemData newItem)
    {
        int result = -1;

        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].uid == newItem.uid)
            {
                result = i;
                break;
            }
        }
        return result;
    }

}
