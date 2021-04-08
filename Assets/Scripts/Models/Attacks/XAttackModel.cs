using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "X Attack", menuName = "Attack/X Attack")]
public class XAttackModel : ScriptableObject
{
    public float damage;
    public float speed;
    public float duration;
}
