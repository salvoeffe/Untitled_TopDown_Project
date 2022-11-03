using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory i;

    [SerializeField] Animator backpackIconAnim;
    public List<Item> inventory = new List<Item>();
    private Dictionary<ItemData, Item> itemDictionary = new Dictionary<ItemData, Item>();

    [SerializeField] GameObject inventorySlotPf;
    [SerializeField] GameObject inventoryUI_GO;

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] AudioClip inventoryToggleSFX;

    private void Awake()
    {
        i = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            RefreshInventory();
        }
    }

    public void Add(ItemData itemData)
    {
        backpackIconAnim.SetTrigger("PlayEffect");
        if(itemDictionary.TryGetValue(itemData, out Item item))
        {
            item.AddToStack();
        }
        else
        {
            Item newItem = new Item(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
        }
    }

    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out Item item))
        {
            item.RemoveFromStack();
            if(item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
        }
    }

    public void ToggleInventory()
    {
        bool toggle = inventoryUI_GO.activeInHierarchy;
        inventoryUI_GO.SetActive(!toggle);
        //playerMovement.SetCanMove(toggle);
        RefreshInventory();
        SoundManager.i.PlaySound(inventoryToggleSFX);
    }

    public void RefreshInventory()
    {
        foreach (Transform child in inventoryUI_GO.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject slot = Instantiate(inventorySlotPf);
            slot.transform.parent = inventoryUI_GO.transform;
            slot.GetComponent<InventorySlot>().SetupSlot(inventory[i]);
            slot.transform.localScale = new Vector3 (1,1,1);
        }
    }
}
