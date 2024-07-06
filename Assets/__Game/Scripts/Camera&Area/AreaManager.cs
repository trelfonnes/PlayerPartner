using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AreaManager : MonoBehaviour
{
    [SerializeField] AreaType thisAreaType;
    [SerializeField] int enemiesInThisArea;
    [SerializeField] int totalEnemiesDefeated;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] EnemiesInAreaSO enemiesInAreaSO;
    [SerializeField] PolygonCollider2D areaTriggerCollider;
    EnemySpawnManager spawnManager;
    int hourAllEnemiesDefeated;
    public bool hasSpawned;
    public bool noEnemySpawnArea;
    private void Awake()
    {
        spawnManager = new EnemySpawnManager();
        areaTriggerCollider = GetComponent<PolygonCollider2D>();
    }
    private void OnEnable()
    {
        Enemy.onEnemyDefeated += EnemyInAreaDefeated;
    }

    void EnemyInAreaDefeated(AreaType areaType)
    {
        if (thisAreaType == areaType)
        {
            totalEnemiesDefeated++;

            if (totalEnemiesDefeated >= enemiesInThisArea)
            {


                hourAllEnemiesDefeated = TimeOfDayManager.Instance.HoursPassed;
                hourAllEnemiesDefeated += 6; //pass how many hours until can respawn.
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!noEnemySpawnArea)
        {
            if (!collision.isTrigger && (collision.CompareTag("Player") || !collision.isTrigger && collision.CompareTag("Partner")))
            {

                if (hourAllEnemiesDefeated <= TimeOfDayManager.Instance.HoursPassed)
                {
                    totalEnemiesDefeated = 0;
                }
                int difference = enemiesInThisArea - totalEnemiesDefeated;
                if (!hasSpawned)
                {
                    for (int i = 0; i < difference; i++)
                    {
                        
                        float rand = UnityEngine.Random.value * 100; // Random value between 0 and 100

                        EnemyType enemyType = EnemyType.RatOne; //shouldn't be called but is set to a default because the compiler complains

                        if (rand < 70f)
                        {
                            enemyType = enemiesInAreaSO.commonAreaEnemies[UnityEngine.Random.Range(0, enemiesInAreaSO.commonAreaEnemies.Count)]; //returns a random one in the list from 0 to length of the list
                        }
                        if (rand >= 70f && rand < 95f)
                        {
                            enemyType = enemiesInAreaSO.uncommonAreaEnemies[UnityEngine.Random.Range(0, enemiesInAreaSO.uncommonAreaEnemies.Count)];
                        }
                        if (rand >= 95f)
                        {
                            enemyType = enemiesInAreaSO.rareAreaEnemies[UnityEngine.Random.Range(0, enemiesInAreaSO.rareAreaEnemies.Count)];
                        }
                        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];

                        // Use spawnManager to spawn the enemy with the chosen type and spawn point
                        spawnManager.SpawnEnemy(enemyType, spawnPoint, thisAreaType);
                        hasSpawned = true;
                    }
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!noEnemySpawnArea) 
        {
            
            if (!collision.isTrigger && collision.CompareTag("Player"))
            {
                if (hasSpawned)
                {
                    hasSpawned = false;
                    foreach (EnemyType item in enemiesInAreaSO.commonAreaEnemies)
                    {
                        spawnManager.ClearEnemy(item);

                    }
                    foreach (EnemyType item in enemiesInAreaSO.uncommonAreaEnemies)
                    {
                        spawnManager.ClearEnemy(item);

                    }
                    foreach (EnemyType item in enemiesInAreaSO.rareAreaEnemies)
                    {
                        spawnManager.ClearEnemy(item);

                    }
                }
            }
    } }
    private void OnDisable()
    {
        Enemy.onEnemyDefeated -= EnemyInAreaDefeated;

    }
}
