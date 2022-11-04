using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public bool canBeSoldByPlayer = false;
    public bool isHat = false;
    public bool isJacket = false;
    public int marketValue = 0;
}
