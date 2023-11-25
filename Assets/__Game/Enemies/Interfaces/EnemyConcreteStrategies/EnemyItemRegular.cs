using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemRegular : IEnemyItemSpawn
{
    int counter = 0;
    public void SpawnItem(Transform spawnPoint)
    {
        if (counter < 1)
        {


            Debug.Log("Enemy SPAWN point: " + spawnPoint);
            GameManager.Instance.SwitchToRegularStrategy();
            ItemSpawnSystem.Instance.SpawnItem(spawnPoint);
            counter++;
        }
    }

}
