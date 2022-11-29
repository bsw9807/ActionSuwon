using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public interface IBaseTownPopup
{
    public void PopupOpen();
    public void PopupClose();
}


public class ShopPopup : MonoBehaviour, IBaseTownPopup
{
    [SerializeField]    private GameObject          shopSlotPrefab;
    [SerializeField]    private RectTransform       contentRect;
    [SerializeField]    private TextMeshProUGUI     balanceText;
    [SerializeField]    private TextMeshProUGUI     amountText;
    private Inventory           inventory;
    private ShopSlot            shopSlot;
    List<ShopSlot> slotList = new List<ShopSlot> ();

    [SerializeField]    private GameObject          sellPage;
    [SerializeField]    private GameObject          buyPage;




    private void Awake()
    {
        gameObject.LeanScale(Vector3.zero, 0.01f);
        InitPopup();
    }

    private void InitPopup()
    {
        inventory = GameManager.Inst.Inventory;
        for (int i = 0; i < inventory.MAXSLOTCOUNT; i++)
        {
            shopSlot = Instantiate(shopSlotPrefab, contentRect).GetComponent<ShopSlot>();
            shopSlot.CreateSlot(this, i);
            slotList.Add(shopSlot);
        }
    }

    public void PopupClose()
    {
        gameObject.LeanScale(Vector3.zero, 0.7f).setEase(LeanTweenType.easeInOutElastic);
    }

    public void PopupOpen()
    {
        gameObject.LeanScale(Vector3.one, 0.7f).setEase(LeanTweenType.easeInOutElastic);
        RefreshData();
    }

    private int sellTotalGold;

    public void CalculateGold()
    {
        sellTotalGold = 0;
        for(int i = 0;  i < slotList.Count; i++)
        {
            if(slotList[i].isActiveAndEnabled)
            {
                sellTotalGold += slotList[i].TOTALGOLD;
            }
        }
    }

    public void RefreshGold()
    {
        CalculateGold();
        amountText.text = sellTotalGold.ToString();
    }


    List<InventoryItemData> dataList;

    private void RefreshData()
    {
        balanceText.text = GameManager.Inst.PlayerGold.ToString(); // 잔고 갱신
        dataList = inventory.GetItemList();

        for(int i = 0; i < inventory.MAXSLOTCOUNT; i++)
        {
            if (i < inventory.CURSLOTCOUNT && -1 < dataList[i].uid)
            {// 현재 슬롯에 아이템이 있다면 
                slotList[i].RefreshSlot(dataList[i]);
            }
            else
                slotList[i].ClearSlot();
        }
        sellTotalGold = 0;
        amountText.text = "0";
    }

    public void SellBtn()
    {
        for(int i = inventory.CURSLOTCOUNT - 1; i >= 0; i--)
        {
            slotList[i].GetSellCount(out int uid, out int sellCount, out int sellGold);

            GameManager.Inst.PlayerGold += sellGold;

            InventoryItemData data = new InventoryItemData();
            data.uid = uid;
            data.amount = sellCount;
            inventory.DeleteItem(data);
        }
        GameManager.Inst.SaveData();
        RefreshData();
    }

    public void SellTapOpen()
    {
        RefreshData();
        sellPage.SetActive(true);
        buyPage.SetActive(false);
    }
    public void BuyTapOpen()
    {
        sellPage.SetActive(false);
        buyPage.SetActive(true);
    }
}
