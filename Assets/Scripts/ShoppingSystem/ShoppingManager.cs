using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShoppingManager : MonoBehaviour
{
    public static ShoppingManager i;
    [SerializeField] GameObject shopKeeperUI_GO;
    [SerializeField] GameObject shoppingChoicesUI;

    bool playerIsSelling = false;
    bool playerIsBuying = false;

    [SerializeField] GameObject playerInventory_UI;
    [SerializeField] GameObject shopInventory_UI;
    [SerializeField] GameObject confirmationWindow;
    [SerializeField] GameObject gameUI;

    [SerializeField] GameObject shopKeeperVCAM;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] AudioClip itemSoldSFX;

    ItemData selectedItemData;
    TextMeshProUGUI confirmationText;
    Button confirmationButton;

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        confirmationText = confirmationWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        confirmationButton = confirmationWindow.transform.GetChild(1).GetComponent<Button>();
    }

    public void ShowShoppingChoices()
    {
        playerIsBuying = false;
        playerIsSelling = false;

        gameUI.SetActive(false);
        shopKeeperUI_GO.SetActive(true);
        shoppingChoicesUI.SetActive(true);
        DeactivateSellAndBuyUI();
    }

    void DeactivateSellAndBuyUI()
    {
        playerInventory_UI.SetActive(false);
        shopInventory_UI.SetActive(false);
        confirmationWindow.SetActive(false);
    }

    public void BuyItems()
    {
        playerIsBuying = true;

        shoppingChoicesUI.SetActive(false);
        gameUI.SetActive(true);
        playerInventory_UI.SetActive(true);
        shopInventory_UI.SetActive(true);
    }

    public void SellItems()
    {
        playerIsSelling = true;

        shoppingChoicesUI.SetActive(false);
        gameUI.SetActive(true);
        playerInventory_UI.SetActive(true);
    }

    public void ExitShopOrGoToChoices()
    {
        if(playerIsSelling || playerIsBuying)
        {
            ShowShoppingChoices();
        }
        else
        {
            ExitShop();
        }
    }

    private void ExitShop()
    {
        shopKeeperUI_GO.SetActive(false);
        gameUI.SetActive(true);
        playerInventory_UI.SetActive(false);
        shopKeeperVCAM.SetActive(false);
        playerMovement.SetCanMove(true);
        playerIsBuying = false;
        playerIsSelling = false;
    }

    public void TradeItem(Item item, bool isInShopInventory)
    {
        ItemData itemData = item.itemData;
        selectedItemData = itemData;

        if (playerIsSelling)
        {
            AskConfirmationToSell(itemData);
        }

        if (playerIsBuying && isInShopInventory)
        {
            AskConfirmationToBuy(itemData);
        }
    }

    private void AskConfirmationToBuy(ItemData itemData)
    {
        confirmationWindow.SetActive(true);
        if (itemData.marketValue > MoneyManager.i.GetCurrentMoney())
        {
            confirmationText.text = "The " + itemData.itemName + " costs " + itemData.marketValue + "$. I'm afraid you don't have enough money to buy it.";
            confirmationButton.interactable = false;
        }
        else
        {
            confirmationText.text = "This " + itemData.itemName + " costs " + itemData.marketValue + "$. Would you like to buy it?";
            confirmationButton.interactable = true;
        }
    }

    private void AskConfirmationToSell(ItemData itemData)
    {
        if (itemData.canBeSoldByPlayer)
        {
            confirmationWindow.SetActive(true);
            confirmationText.text = "Would you like to sell this " + itemData.itemName + " for " + itemData.marketValue + "$?";
            confirmationButton.interactable = true;
        }
        else
        {
            confirmationWindow.SetActive(true);
            confirmationText.text = "I'm not buying the " + itemData.itemName + ". I just sold it to you!";
            confirmationButton.interactable = false;
        }

    }

    public void Confirm()
    {
        if (playerIsSelling && selectedItemData != null)
        {
            Inventory.i.Remove(selectedItemData);
            MoneyManager.i.AddMoney(selectedItemData.marketValue);
        }

        if (playerIsBuying && selectedItemData != null)
        {
            Inventory.i.Add(selectedItemData);
            MoneyManager.i.RemoveMoney(selectedItemData.marketValue);
            RemoveItemFromShopInventory(selectedItemData);
        }

        Inventory.i.RefreshInventory();
        SoundManager.i.PlaySound(itemSoldSFX);
        confirmationText.text = "Pleasure doing business with you!";
        selectedItemData = null;
        confirmationButton.interactable = false;
    }

    private void RemoveItemFromShopInventory(ItemData itemData)
    {
        foreach (Transform child in shopInventory_UI.transform)
        {
            ShopSlot shopSlot = child.GetComponent<ShopSlot>();
            if(shopSlot != null && shopSlot.GetItemData() == itemData)
            {
                Destroy(shopSlot.gameObject);
            }
        }
    }
}
