using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] ItemData thisItemData;

    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI stackNumberText;
    [SerializeField] TextMeshProUGUI itemNameText;
    bool isInShopInventory = true;

    private void Start()
    {
        SetupShopSlot();
    }

    public void SetupShopSlot()
    {
        itemIcon.sprite = thisItemData.itemIcon;
        stackNumberText.text = "1";
        itemNameText.text = thisItemData.itemName;
    }

    public void TradeThisItem()
    {
        Item itemToPass = new Item(thisItemData);
        itemToPass.stackSize = 1;
        ShoppingManager.i.TradeItem(itemToPass, isInShopInventory);
    }

    public ItemData GetItemData()
    {
        return thisItemData;
    }
}
