using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerModel : CharacterModel
{
    public float specialAttackTimer;
    public float horizontalSpeedAirFactor;
    public float ladderSpeed;
    public bool hasDoubleJump;
    public EPlayerType type;
}
