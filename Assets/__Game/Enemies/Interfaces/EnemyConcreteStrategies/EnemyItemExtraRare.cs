using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemExtraRare : IEnemyItemSpawn
{
    public void SpawnItem(Transform spawnPoint)
    {
        GameManager.Instance.SwitchToExtraRareStrategy();
        ItemSpawnSystem.Instance.SpawnItem(spawnPoint);

    }

}
