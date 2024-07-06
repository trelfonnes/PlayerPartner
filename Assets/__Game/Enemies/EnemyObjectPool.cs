using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPrefabEntry
{
    public EnemyType enemyType;
    public GameObject enemyPrefab;
}
[System.Serializable]
public class EnemyPoolSizeEntry
{
    public EnemyType enemyType;
    public int poolSize;
}

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField] private List<EnemyPrefabEntry> enemyPrefabs = new List<EnemyPrefabEntry>();
    [SerializeField] private List<EnemyPoolSizeEntry> poolSizes = new List<EnemyPoolSizeEntry>();

    private Dictionary<EnemyType, GameObject> enemyPrefabDictionary = new Dictionary<EnemyType, GameObject>();
    private Dictionary<EnemyType, int> enemyPoolSizeDictionary = new Dictionary<EnemyType, int>();

    private Dictionary<GameObject, List<GameObject>> pooledEnemyObjectsDictionary = new Dictionary<GameObject, List<GameObject>>();
    Transform locationToPlace;

    private void Awake()
    {
        InitializeProjectileDictionaries();
    }
    private void Start()
    {
        CreatePools();   
    }
    void InitializeProjectileDictionaries()
    {
        foreach(EnemyPrefabEntry entry in enemyPrefabs)
        {
            enemyPrefabDictionary[entry.enemyType] = entry.enemyPrefab;
        }
        foreach(EnemyPoolSizeEntry entry in poolSizes)
        {
            enemyPoolSizeDictionary[entry.enemyType] = entry.poolSize;
        }
    }
    void CreatePools()
    {
        foreach (KeyValuePair<EnemyType, GameObject> kvp in enemyPrefabDictionary)
        {
            if (!pooledEnemyObjectsDictionary.ContainsKey(kvp.Value))
            {
                List<GameObject> objects = new List<GameObject>();
                int poolSize = enemyPoolSizeDictionary[kvp.Key];
                for (int j = 0; j < poolSize; j++)
                {
                    GameObject obj = Instantiate(kvp.Value);
                    obj.SetActive(false);
                    objects.Add(obj);

                }
                pooledEnemyObjectsDictionary.Add(kvp.Value, objects);
            }
            else
            {
                Debug.LogWarning("Duplicate key found: " + kvp.Value);
            }
        
        }
    }
    private GameObject GetPrefabFromType(EnemyType enemyType)
    {
        if(enemyPrefabDictionary.TryGetValue(enemyType, out GameObject prefab))
        {
            return prefab;
        }
        else
        {
            return null;
        }
    }
    public GameObject GetPooledEnemy(EnemyType enemyType, Transform spawnLocation)
    {
        GameObject prefab = GetPrefabFromType(enemyType);
        if(prefab !=null && pooledEnemyObjectsDictionary.ContainsKey(prefab))
        {
            List<GameObject> pooledObjects = pooledEnemyObjectsDictionary[prefab];

            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    pooledObjects[i].SetActive(true);
                    SetEnemyPosition(pooledObjects[i], spawnLocation.position, spawnLocation.rotation);
                    return pooledObjects[i];
                }

            }
        }
        else//make a new one then add it to the dictionary
        {
            GameObject newObj = Instantiate(prefab);
            SetEnemyPosition(newObj, spawnLocation.position, spawnLocation.rotation);
            if (!pooledEnemyObjectsDictionary.ContainsKey(prefab))
            {
                pooledEnemyObjectsDictionary.Add(prefab, new List<GameObject>());
            }
            pooledEnemyObjectsDictionary[prefab].Add(newObj);
            return newObj;
        }
        return null;
    } 
    public GameObject GetPooledEnemyWithArea(EnemyType enemyType, Transform spawnLocation, AreaType area)
    {
        GameObject prefab = GetPrefabFromType(enemyType);
        if (prefab == null)
        {
            Debug.LogError("Prefab is null for enemy type: " + enemyType);
            return null;
        }

        if (!pooledEnemyObjectsDictionary.ContainsKey(prefab))
        {
            pooledEnemyObjectsDictionary[prefab] = new List<GameObject>();
        }

        List<GameObject> pooledObjects = pooledEnemyObjectsDictionary[prefab];

        // Look for an inactive pooled object
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(true);
                SetEnemyPosition(pooledObjects[i], spawnLocation.position, spawnLocation.rotation);
                Enemy enemy = pooledObjects[i].GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.InitializeAreaType(area);
                }
                return pooledObjects[i];
            }
        }

        // If no inactive object is found, instantiate a new one
        GameObject newObj = Instantiate(prefab);
        SetEnemyPosition(newObj, spawnLocation.position, spawnLocation.rotation);
        pooledEnemyObjectsDictionary[prefab].Add(newObj);

        Enemy newEnemy = newObj.GetComponent<Enemy>();
        if (newEnemy != null)
        {
            newEnemy.InitializeAreaType(area);
        }

        return newObj;
    }
    private void SetEnemyPosition(GameObject enemy, Vector3 position, Quaternion rotation)
    {
        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
    }
    public void ReceiveEnemyTypeToUnpool(EnemyType type, Transform location)
    {
        GetPooledEnemy(type, location);

    }
    public void ReceiveEnemyTypeAndArea(EnemyType type, Transform location, AreaType area)
    {
        GetPooledEnemyWithArea(type, location, area);
    }
    public void ReceiveTypeToPool(EnemyType type)
    {
        if (enemyPrefabDictionary.ContainsKey(type))
        {


            GameObject enemyPrefab = enemyPrefabDictionary[type];
            if (pooledEnemyObjectsDictionary.ContainsKey(enemyPrefab))
            {
                List<GameObject> pooledObjects = pooledEnemyObjectsDictionary[enemyPrefab];
                foreach (GameObject obj in pooledObjects)
                {
                    if (obj.activeInHierarchy)
                    {
                        obj.SetActive(false);
                    }
                }
            }
        }
    }
    private void OnEnable()
    {
        if (EnemyPoolManager.Instance != null)
        {
            EnemyPoolManager.Instance.onEnemyTypeLocationAreaToSpawn += ReceiveEnemyTypeAndArea;
            EnemyPoolManager.Instance.onEnemyTypeAndLocationToSpawn += ReceiveEnemyTypeToUnpool;
            EnemyPoolManager.Instance.onClearEnemies += ReceiveTypeToPool;
        }
        else
        {
            Debug.LogError("EnemyPoolManager is null");
        }
    }
    private void OnDisable()
    {
        if (EnemyPoolManager.Instance != null)
        {
            EnemyPoolManager.Instance.onEnemyTypeLocationAreaToSpawn -= ReceiveEnemyTypeAndArea;

            EnemyPoolManager.Instance.onEnemyTypeAndLocationToSpawn -= ReceiveEnemyTypeToUnpool;
            EnemyPoolManager.Instance.onClearEnemies -= ReceiveTypeToPool;
        }
       
    }
}


