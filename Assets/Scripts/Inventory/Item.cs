using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public float currentCD;
    public int currentAmount;

    public abstract void Use();

    public virtual void UpdateCD()
    {
        currentCD -= Time.deltaTime;
    }
    public virtual void PickUp()
    {
        currentAmount++;
    }
}
