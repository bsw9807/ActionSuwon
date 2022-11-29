using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    private bool isEmpty;
    public bool EMPTY
    { get => isEmpty; }

    private Image icon;
    private GameObject focus;
    private TextMeshProUGUI amount;
    private Image amountBackground;

    private void Awake()
    {
        icon = transform.GetChild(1).GetComponent<Image>();
        focus = transform.GetChild(0).gameObject;
        amount = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        amountBackground = transform.GetChild(2).GetComponent<Image>();
        ClearSlot();
    }

    Color iconColor;
    public void ClearSlot()
    {
        focus.SetActive(false);
        amount.enabled = false;
        isEmpty = true;
        iconColor = icon.color;
        iconColor.a = 0f;
        icon.color = iconColor;

        iconColor = amountBackground.color;
        iconColor.a = 0f;
        amountBackground.color = iconColor;
    }

    public void DrawItem(InventoryItemData newItem)
    {
        GameManager.Inst.GetItemData(newItem.uid, out TableItem item);
        icon.sprite = Resources.Load<Sprite>(item.iconImg);
        ChangeAmount(newItem.amount);
        isEmpty = false;
        iconColor = icon.color;
        iconColor.a = 1f;
        icon.color = iconColor;

        if (newItem.amount > 1)
        {
            iconColor = amountBackground.color;
            iconColor.a = 1f;
            amountBackground.color = iconColor;

            amount.enabled = true;
            amount.text = newItem.amount.ToString();
        }
    }

    public void ChangeAmount(int newAmount)
    {
        amount.text = newAmount.ToString();
    }
    public void SelectSlot(bool isSelect)
    {
        focus.SetActive(isSelect);
    }
}
