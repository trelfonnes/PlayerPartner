using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    public event Action<EnemyType, Transform> onEnemyTypeAndLocationToSpawn;
    public event Action<EnemyType, Transform, AreaType> onEnemyTypeLocationAreaToSpawn;
    public event Action<EnemyType> onClearEnemies;
    public event Action<Transform> onLocationToSpawnEnemy;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
   
    public void RaiseEnemyTypeAndLocationToSpawn(EnemyType type, Transform location)
    {
        onEnemyTypeAndLocationToSpawn?.Invoke(type, location);
    }
    public void RaiseEnemyTypeLocationAndArea(EnemyType type, Transform location, AreaType area)
    {
        onEnemyTypeLocationAreaToSpawn?.Invoke(type, location, area);
    }
    public void RaiseClearEnemies(EnemyType type)
    {

        onClearEnemies?.Invoke(type);
    }

}
