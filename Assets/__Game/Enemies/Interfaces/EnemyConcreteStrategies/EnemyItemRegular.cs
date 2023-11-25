using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemRegular : IEnemyItemSpawn
{
    public void SpawnItem(Transform spawnPoint)
    {
        Debug.Log("Enemy SPAWN point: " + spawnPoint);
        GameManager.Instance.SwitchToRegularStrategy();
        ItemSpawnSystem.Instance.SpawnItem(spawnPoint);

    }

}
