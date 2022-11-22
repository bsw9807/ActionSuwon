using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    Inventory inventory = new Inventory();

    public void GetItem(InventoryItemData item)
    {
        inventory.AddItem(item);
    }

}
