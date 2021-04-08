using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShoot", menuName = "Enemy/Enemy Shoot Model")]
public class EnemyShootsModel : ScriptableObject
{
    public new string name;
    public Sprite image;
    public float damage;
    public float speed;
    public float x, y;
}
