using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClothing : MonoBehaviour
{
    public static PlayerClothing i;

    [SerializeField] SpriteRenderer hatSpriteRenderer;
    [SerializeField] SpriteRenderer jacketSpriteRenderer;
    [SerializeField] AudioClip wearClothesSFX;

    private void Awake()
    {
        i = this;
    }

    public void WearItem(Item item)
    {
        ItemData itemData = item.itemData;

        if (itemData.isHat)
        {
            WearHat(itemData.itemIcon);
        }
        else if (itemData.isJacket)
        {
            WearJacket(itemData.itemIcon);
        }
    }

    private void WearJacket(Sprite itemIcon)
    {
        if(jacketSpriteRenderer.sprite == itemIcon)
        {
            jacketSpriteRenderer.sprite = null;
        }
        else
        {
            jacketSpriteRenderer.sprite = itemIcon;
        }
        SoundManager.i.PlaySound(wearClothesSFX);
    }

    private void WearHat(Sprite itemIcon)
    {
        if(hatSpriteRenderer.sprite == itemIcon)
        {
            hatSpriteRenderer.sprite = null;
        }
        else
        {
            hatSpriteRenderer.sprite = itemIcon;
        }
        SoundManager.i.PlaySound(wearClothesSFX);
    }
}
