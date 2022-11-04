using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    Item thisItem;

    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI stackNumberText;
    [SerializeField] TextMeshProUGUI itemNameText;
    bool isInShopInventory = false;

    public void SetupSlot(Item item)
    {
        thisItem = item;
        ItemData itemData = item.itemData;
        itemIcon.sprite = itemData.itemIcon;
        stackNumberText.text = item.stackSize.ToString();
        itemNameText.text = itemData.itemName;
    }

    public void TradeOrWearThisItem()
    {
        if (thisItem.itemData.isHat || thisItem.itemData.isJacket)
        {
            PlayerClothing.i.WearItem(thisItem);
        }
        ShoppingManager.i.TradeItem(thisItem, isInShopInventory);
    }
}
