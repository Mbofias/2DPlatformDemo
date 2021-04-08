using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveData
{
    public bool trainEventCompleted, shieldEnabled, isZero;
    public int cameraSize, bombsAmount, aerialAmount, shieldAmount;
    public float playerHealth, playerShield, bombCD, aerialCD, shieldCD, playerSpawnX, playerSpawnY, camMinX, camMinY, camMaxX, camMaxY;

    public SaveData(LevelManager level, GameManager instance)
    {
        trainEventCompleted = level.TrainEventCompleted;
        playerSpawnX = level.playerSpawnPoint.position.x;
        playerSpawnY = level.playerSpawnPoint.position.y;
        camMinX = level.Cam.GetCameraBounds().min.x;
        camMinY = level.Cam.GetCameraBounds().min.y;
        camMaxX = level.Cam.GetCameraBounds().max.x;
        camMaxY = level.Cam.GetCameraBounds().max.y;
        cameraSize = level.CameraSize;
        shieldEnabled = instance.onEnergyOverload;
        bombsAmount = instance.inventory.bomb.currentAmount;
        aerialAmount = instance.inventory.aerialAttack.currentAmount;
        shieldAmount = instance.inventory.energyOverload.currentAmount;
        playerHealth = instance.playerHealth;
        playerShield = instance.playerEnergy;
        bombCD = instance.inventory.bomb.currentCD;
        aerialCD = instance.inventory.aerialAttack.currentCD;
        shieldCD = instance.inventory.energyOverload.currentCD;

        if (instance.playerType == EPlayerType.ZERO)
            isZero = true;
        else
            isZero = false;
    }
}