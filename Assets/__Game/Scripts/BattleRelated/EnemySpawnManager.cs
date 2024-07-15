using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemySpawnInformation
{
    public EnemyType enemyType;
    public Transform spawnPoint;
}

public class EnemySpawnManager 
{

    //for Battle and wave manager
    public void HandleWaveStart(EnemySpawnInformation enemySpawnInfo)
    {
        
        EnemyPoolManager.Instance.RaiseEnemyTypeAndLocationToSpawn(enemySpawnInfo.enemyType, enemySpawnInfo.spawnPoint);
             
    }
    public void SpawnEnemies(EnemySpawnInformation enemySpawnInfo) //to use, create an instance of this class and call this method
    {//e.g. a room in a dungeon can call this when it is entered to spawn enemies from the pool.
        
        EnemyPoolManager.Instance.RaiseEnemyTypeAndLocationToSpawn(enemySpawnInfo.enemyType, enemySpawnInfo.spawnPoint);
        

    }
    public void SpawnEnemy(EnemyType enemyType, Transform spawnLocation, AreaType areaType)
    {
        
        EnemyPoolManager.Instance.RaiseEnemyTypeLocationAndArea(enemyType, spawnLocation, areaType);
    }

    public void ClearEnemies(EnemySpawnInformation enemySpawnInfo)
    {

        EnemyPoolManager.Instance.RaiseClearEnemies(enemySpawnInfo.enemyType);
    }
    public void ClearEnemy(EnemyType enemyType)
    {
        EnemyPoolManager.Instance.RaiseClearEnemies(enemyType);

    }
}
