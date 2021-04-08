using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Defensive Item", menuName = "Inventory/Defensive Item")]
public class DefensiveModel : ItemModel
{
    public float shield;
    public float reductionFactor;

    private void Awake()
    {
        reductionFactor = shield / duration;
    }
}
