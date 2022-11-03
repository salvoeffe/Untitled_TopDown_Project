using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory i;
    public List<Item> inventory = new List<Item>();
    private Dictionary<ItemData, Item> itemDictionary = new Dictionary<ItemData, Item>();

    private void Awake()
    {
        i = this;
    }

    public void Add(ItemData itemData)
    {
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
}
