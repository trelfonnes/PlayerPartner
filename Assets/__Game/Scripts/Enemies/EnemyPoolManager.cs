using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance { get; private set; }

    public event Action<EnemyType, Transform> onEnemyTypeAndLocationToSpawn;
    public event Action<Transform> onLocationToSpawnEnemy;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public void RaiseEnemyTypeAndLocationToSpawn(EnemyType type, Transform location)
    {
        onEnemyTypeAndLocationToSpawn?.Invoke(type, location);
    }


}
