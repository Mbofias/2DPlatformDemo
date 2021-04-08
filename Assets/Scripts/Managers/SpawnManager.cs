using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies, items, players;
    public GameObject boss;

    /// <summary>
    /// Spawns a given enemy.
    /// </summary>
    /// <param name="enemyID">0.Basic air enemy, 1.Special air enemy, 2.Moving land enemy, 3.Static land enemy</param>
    /// <param name="position">The position where the player should be spawned at.</param>
    /// <param name="parent">A transform in case the enemy should be the child of another GameObject (null by default)</param>
    public void SpawnEnemy (int enemyID, Transform parent = null)
    {
        GameObject tmp = Instantiate(enemies[enemyID], parent);
        tmp.transform.parent = null;
    }

    /// <summary>
    /// Spawns the player.
    /// </summary>
    /// <param name="position">The position where the player should be spawned at.</param>
    public void SpawnPlayer(Vector2 position)
    {
        if (GameManager.Instance.playerType == EPlayerType.ZERO)
            Instantiate(players[0], position, new Quaternion(0, 0, 0, 0));
        else
            Instantiate(players[1], position, new Quaternion(0, 0, 0, 0));
    }

    public void SpawnBoss(Vector2 position)
    {
        Instantiate(boss, position, new Quaternion(0, 0, 0, 0));
    }
}
