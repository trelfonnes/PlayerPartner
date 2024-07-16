using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using PixelCrushers.DialogueSystem;

public class WaveBattleManager : MonoBehaviour
{
   [System.Serializable]
   public class Wave
    {
        public int numberOfEnemies;
        public List<EnemySpawnInformation> enemySpawns;
    }

    [SerializeField] Wave[] waves;

    public Transform[] spawnPoints;
    public UnityEvent onWaveCompleted;
    public UnityEvent onBattleCompleted; // use to open the entrance to knothole villiage.
    int currentWaveIndex = 0;
    int enemiesRemaining = 0;
    EnemySpawnManager spawnManager;

    private void Start()
    {
        spawnManager = new EnemySpawnManager();
    }

    public void StartWaveBattle()
    {
        currentWaveIndex = 0;
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        if(currentWaveIndex < waves.Length)
        {
            Wave wave = waves[currentWaveIndex];
            enemiesRemaining = wave.numberOfEnemies;
            foreach (var spawnInfo in wave.enemySpawns)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                spawnInfo.spawnPoint = spawnPoint;
                spawnManager.HandleWaveStart(spawnInfo);
            }
            
            yield return null;
        }
        else
        {
            onBattleCompleted?.Invoke();
        }
    }
    public void OnEnemyDefeated()
    {
        enemiesRemaining--;
        if(enemiesRemaining <= 0)
        {
            currentWaveIndex++;
            onWaveCompleted?.Invoke();
            StartCoroutine(StartNextWave());
        }
    }
    private void OnEnable()
    {
        Lua.RegisterFunction("StartWaveBattle", this, SymbolExtensions.GetMethodInfo(() => StartWaveBattle()));

    }
    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(StartWaveBattle));
    }
}
