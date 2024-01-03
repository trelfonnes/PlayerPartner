using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class EnemyWaveManager
{
    public event Action<EnemySpawnInformation> onWaveStart;
    List<EnemySpawnInformation> enemyWaveInfo;
    int currentWaveIndex = 0;
   
    public void Initialize(List<EnemySpawnInformation> waveInfo)
    {
        enemyWaveInfo = waveInfo;
    }


    public void StartNextWave()
    {
        if(currentWaveIndex < enemyWaveInfo.Count)
        {
            EnemySpawnInformation currentWave = enemyWaveInfo[currentWaveIndex];
            onWaveStart?.Invoke(currentWave);
            currentWaveIndex++;
        }
        else
        {
            currentWaveIndex = 0;
            //all waves completed
        }
    }
    


}
