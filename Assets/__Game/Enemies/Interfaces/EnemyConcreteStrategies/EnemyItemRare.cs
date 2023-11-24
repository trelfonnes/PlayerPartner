using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemRare : IEnemyItemSpawn
{
    public void SpawnItem(Transform spawnPoint)
    {
        GameManager.Instance.SwitchToRareStrategy();
        ItemSpawnSystem.Instance.SpawnItem(spawnPoint);

    }

}
