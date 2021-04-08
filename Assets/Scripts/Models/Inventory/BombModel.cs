using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bomb", menuName = "Inventory/Bomb")]
public class BombModel : ExplosiveModel
{
    public float timeToExplosion;
    public float throwFactorHorizontal, throwFactorVertical;
}
