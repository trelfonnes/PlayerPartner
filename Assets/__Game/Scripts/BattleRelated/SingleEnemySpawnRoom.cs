using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleEnemySpawnRoom : MonoBehaviour
{
    [SerializeField] List<EnemySpawnInformation> enemiesSpawnInfo;
    EnemySpawnManager spawnManager;
    int enemyCount = 0;

    private void Start()
    {
        spawnManager = new EnemySpawnManager();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyCount <= enemiesSpawnInfo.Count)
        {
            foreach (EnemySpawnInformation item in enemiesSpawnInfo)
            {
                spawnManager.SpawnEnemies(item);
                enemyCount++;
            }
        }
       
        
    }
    private void OnTriggerExit(Collider other)
    {

        foreach (EnemySpawnInformation item in enemiesSpawnInfo) 
        {
            spawnManager.ClearEnemies(item);
            enemyCount = 0;
        }
    }
}
