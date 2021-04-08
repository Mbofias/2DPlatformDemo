using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel : ScriptableObject
{
    public Sprite img;
    public string itemName, description;
    [Range(1,3)] public int maxAmount;
    public float duration, cooldown;
}
