using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public ItemData itemData;
    [SerializeField] AudioClip pickUpItemSFX;

    public void PickUpItem()
    {
        Inventory.i.Add(itemData);
        Inventory.i.RefreshInventory();
        SoundManager.i.PlaySound(pickUpItemSFX);
        Destroy(gameObject);
    }
}
