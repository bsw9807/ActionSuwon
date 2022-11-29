using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    private ShopPopup shopPopup;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private TextMeshProUGUI itemPriceText;
    [SerializeField]
    private TextMeshProUGUI sellCountText;

    [SerializeField]
    private Button right;
    [SerializeField]
    private Button left;
    [SerializeField]
    private Button max;

    private InventoryItemData data;

    private int slotIndex;
    public int SLOTINDEX
    {
        get => slotIndex;
    }


    private int sellGold; // 판매에 대한 단위 금액
    private int sellMaxCount; // 팔수있는 최대 갯수
    private int curCount; // 화살표 조작에 의해서 갱신된 갯수

    private int totalGold;
    public int TOTALGOLD
    {
        get => totalGold;
    }

    private int uid;

    private void Awake()
    {
        right.onClick.AddListener(OnClickRightBtn);
        left.onClick.AddListener(OnClickLeftBtn);
        max.onClick.AddListener(OnClickMaxBtn);
    }

    public void CreateSlot(ShopPopup popup , int Index)
    {
        shopPopup = popup;
        slotIndex = Index;
        gameObject.SetActive(false);
    }

    public void ClearSlot()
    {
        gameObject.SetActive(false);
    }

    Color iconColor;
    public void RefreshSlot(InventoryItemData item)
    {
        uid = item.uid;
        gameObject.SetActive(true);
        curCount = 0;
        sellCountText.text = "0";
        sellMaxCount = item.amount;
        GameManager.Inst.GetItemData(item.uid, out TableItem itemData);
        icon.sprite = Resources.Load<Sprite>(itemData.iconImg);

        itemPriceText.text = itemData.sellGold.ToString();
        sellGold = itemData.sellGold;

        iconColor = icon.color;
        iconColor.a = 1f;
        icon.color = iconColor;
    }






    #region btn
    public void OnClickRightBtn()
    {
        if (sellMaxCount > curCount)
            curCount++;

        sellCountText.text = curCount.ToString();
        totalGold = sellGold * curCount;
        shopPopup.RefreshGold();

    }
    public void OnClickLeftBtn()
    {
        if (curCount > 0)
            curCount--;
        sellCountText.text = curCount.ToString();
        totalGold = sellGold * curCount;
        shopPopup.RefreshGold();
    }
    public void OnClickMaxBtn()
    {
        curCount = sellMaxCount;
        sellCountText.text = curCount.ToString();
        totalGold = sellGold * curCount;
        shopPopup.RefreshGold();
    }
    #endregion

    public bool GetSellCount(out int _sellUid, out int _sellCount, out int _sellGold)
    {
        _sellUid = uid;
        _sellCount = curCount;
        sellMaxCount -= curCount;
        _sellGold = totalGold;
        return true;
    }

}
