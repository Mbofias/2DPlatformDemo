using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public ExplosiveItem aerialAttack, bomb;
    public DefensiveItem energyOverload;
    private GameObject bombObject, aerialObject;
    private CPlayerController player;

    public Inventory(ExplosiveModel aerial, ExplosiveModel _bomb, DefensiveModel energy, GameObject throwBomb = null, GameObject throwAerial = null)
    {
        bombObject = throwBomb;
        aerialObject = throwAerial;
        ResetInventory(aerial, _bomb, energy);
    }
    
    /// <summary>
    /// Creates an empty inventory.
    /// </summary>
    /// <param name="aerial">Aerial attack model.</param>
    /// <param name="_bomb">Bomb model.</param>
    /// <param name="energy">Energy Overload model.</param>
    public void ResetInventory(ExplosiveModel aerial, ExplosiveModel _bomb, DefensiveModel energy)
    {
        bomb = new ExplosiveItem(_bomb);
        aerialAttack = new ExplosiveItem(aerial);
        energyOverload = new DefensiveItem(energy);
        ResetInventory();
    }

    /// <summary>
    /// Function executed on every update cycle of the game manager, to check if any item is used.
    /// </summary>
    public void Execute()
    {
        if (player != null)
        {
            if (aerialAttack.currentCD > 0)
                aerialAttack.UpdateCD();
            if (energyOverload.currentCD > 0)
                energyOverload.UpdateCD();
            if (bomb.currentCD > 0)
                bomb.UpdateCD();

            if (Input.GetButtonDown("Item1"))
                UseBomb();
            if (Input.GetButtonDown("Item2"))
                UseAerialAttack();
            if (Input.GetButtonDown("Item3"))
                UseEnergyOverload();
        }
    }

    /// <summary>
    /// Uses the aerial attack, if the player has any aerial attack and it's not on CD.
    /// </summary>
    public void UseAerialAttack()
    {
        if (aerialAttack.currentCD <= 0 && aerialAttack.currentAmount > 0)
        {
            aerialAttack.Use();
            player.ThrowObject(aerialObject, false);
        }
    }

    /// <summary>
    /// Uses the energy overload, if the player has any energy overload and it's not on CD.
    /// </summary>
    public void UseEnergyOverload()
    {
        if (energyOverload.currentCD <= 0 && energyOverload.currentAmount > 0)
        {
            energyOverload.Use();

            GameManager.Instance.onEnergyOverload = true;
            GameManager.Instance.playerEnergy = energyOverload.itemData.shield;

            player.SpawnShield();
        }
    }

    /// <summary>
    /// Uses the bomb, if the player has any bomb and it's not on CD.
    /// </summary>
    public void UseBomb()
    { 
        if (bomb.currentCD <= 0 && bomb.currentAmount > 0)
        {
            bomb.Use();
            player.ThrowObject(bombObject,true);
        }
    }

    /// <summary>
    /// This function completely resets the inventory, removing any item and it's CDs.
    /// </summary>
    public void ResetInventory()
    {
        bomb.currentCD = 0;
        bomb.currentAmount = 1;
        aerialAttack.currentCD = 0;
        aerialAttack.currentAmount = 1;
        energyOverload.currentAmount = 1;
        energyOverload.currentCD = 0;
    }

    public void PassPlayerReference(CPlayerController _player)
    {
        player = _player;
    }
}
