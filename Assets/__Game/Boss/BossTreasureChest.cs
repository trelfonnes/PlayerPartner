using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreasureChest : TreasureChest
{
    [SerializeField] EnemyStatEvents currentLevelBossStatEvents;
    [SerializeField] Transform spawnLocation;
    protected override void Start()
    {
        base.Start();
        currentLevelBossStatEvents.onBossEnemyDefeated += SpawnBossChest;
        
    }

    void SpawnBossChest()
    {
        transform.position = spawnLocation.position;
    }
}
