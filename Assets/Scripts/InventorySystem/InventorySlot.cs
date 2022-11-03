using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI stackNumberText;
    [SerializeField] TextMeshProUGUI itemNameText;

    public void SetupSlot(Item item)
    {
        ItemData itemData = item.itemData;
        itemIcon.sprite = itemData.itemIcon;
        stackNumberText.text = item.stackSize.ToString();
        itemNameText.text = itemData.itemName;
    }
}
