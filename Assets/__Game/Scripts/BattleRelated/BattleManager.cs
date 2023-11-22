using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public EnemySpawnManager spawningManager;
    public EnemyWaveManager waveManager;
    [SerializeField] List<EnemySpawnInformation> spawnInformation;
    private BoxCollider2D boundaryCollider;
    private int totalSpawnedEnemies = 0;
    private int totalDefeatedEnemies = 0;
    [SerializeField] float timeBetweenSpawn;
    int numberOfWaves;
    float timeSinceLastWave = 0f;
    bool battleStarted;
    private void Start()
    {
        spawningManager = new EnemySpawnManager();
        waveManager = new EnemyWaveManager();
        waveManager.onWaveStart += spawningManager.HandleWaveStart;
        numberOfWaves = spawnInformation.Count;
        waveManager.Initialize(spawnInformation);
    }
    private void Update()
    {
        if(battleStarted && totalSpawnedEnemies < numberOfWaves)
        {
            timeSinceLastWave += Time.deltaTime;
            if(timeSinceLastWave >= timeBetweenSpawn)
            {
                StartWave();
                timeSinceLastWave = 0f;
                totalSpawnedEnemies++;
            }
        }
        if(totalSpawnedEnemies >= numberOfWaves)
        {
            battleStarted = false;
            totalSpawnedEnemies = 0;
        }
    }
    private void StartWave()
    {           
        waveManager.StartNextWave();
    }
    public void EnemyDefeated()
    {
        totalDefeatedEnemies++;
    }
    private void OnDisable()
    {
        waveManager.onWaveStart -= spawningManager.HandleWaveStart;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Partner"))
        {
            StartBattle();
        }
    }
    void StartBattle()
    {
        battleStarted = true;
    }
}
