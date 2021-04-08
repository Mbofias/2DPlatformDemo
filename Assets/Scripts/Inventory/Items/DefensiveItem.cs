using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveItem : Item
{
    public DefensiveModel itemData;

    public DefensiveItem(DefensiveModel data)
    {
        itemData = GameObject.Instantiate(data);
        currentCD = 0;
        currentAmount = 0;
    }

    public override void Use()
    {
        currentCD = itemData.cooldown;
        currentAmount--;
    }

    public override void PickUp()
    {
        if (currentAmount < itemData.maxAmount)
            base.PickUp();
    }

    public void Execute()
    {

    }
}
