using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public bool canBeSold = false;
    public int marketValue = 0;
}
