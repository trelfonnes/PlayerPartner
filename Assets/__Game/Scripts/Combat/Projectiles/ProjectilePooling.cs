using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectilePrefabEntry //separate classes allow for inspector serialization
{
    public ProjectileType projectileType;
    public GameObject projectilePrefab;
}
[System.Serializable]
public class PoolSizeEntry
{
    public ProjectileType projectileType;
    public int poolSize;
}
public class ProjectilePooling : MonoBehaviour  //inherit from a script that stores all potential projectiles as prefabs. That script looks at a conditional value and dictates which partner projectiles are needed at awake
{
    [SerializeField] private List<ProjectilePrefabEntry> projectilePrefabs = new List<ProjectilePrefabEntry>();
    [SerializeField] private List<PoolSizeEntry> poolSizes = new List<PoolSizeEntry>();

    private Dictionary<ProjectileType, GameObject> projectilePrefabDictionary = new Dictionary<ProjectileType, GameObject>();
    private Dictionary<ProjectileType, int> poolSizeDictionary = new Dictionary<ProjectileType, int>();

    private Dictionary<GameObject, List<GameObject>> pooledObjectsDictionary = new Dictionary<GameObject, List<GameObject>>();

  

    private void Awake()
    {
        InitializeProjectileDictionaries();
    }
    private void Start()
    {
        CreatePools();
    }

    private void InitializeProjectileDictionaries() // creates the dictionaries via whats dropped into inspector
    {
        foreach (ProjectilePrefabEntry entry in projectilePrefabs)
        {
            projectilePrefabDictionary[entry.projectileType] = entry.projectilePrefab;
        }

        foreach (PoolSizeEntry entry in poolSizes)
        {
            poolSizeDictionary[entry.projectileType] = entry.poolSize;
        }
    }
    private void CreatePools() // makes pools of the dictionary types created in inspector
    {
        foreach (KeyValuePair<ProjectileType, GameObject> kvp in projectilePrefabDictionary)
        {
            List<GameObject> objects = new List<GameObject>();
            int poolSize = poolSizeDictionary[kvp.Key];

            for (int j = 0; j < poolSize; j++)
            {
                GameObject obj = Instantiate(kvp.Value);
                obj.SetActive(false);
                objects.Add(obj);
            }

            pooledObjectsDictionary.Add(kvp.Value, objects);
        }
    }

    public GameObject GetPooledObject(ProjectileType projectileType) // iterates over pooled objects of that passed in type and sets reference
    {
        GameObject prefab = GetPrefabFromType(projectileType);

        if (prefab != null && pooledObjectsDictionary.ContainsKey(prefab))
        {
            List<GameObject> pooledObjects = pooledObjectsDictionary[prefab];

            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    pooledObjects[i].SetActive(true);
                    return pooledObjects[i];
                }
            }
        }

        // If no inactive objects are found or the prefab is not in the dictionary, create a new object
        if (projectileType == ProjectileType.PlayerBoomerangProjectile)
        {
            //return null
            return null;
        }
        else //make a new one
        {
            GameObject newObj = Instantiate(prefab);
            if (!pooledObjectsDictionary.ContainsKey(prefab))
            {
                pooledObjectsDictionary.Add(prefab, new List<GameObject>());
            }
            pooledObjectsDictionary[prefab].Add(newObj);

            return newObj;
        }
    }

    private GameObject GetPrefabFromType(ProjectileType projectileType) //gets from dictionary based on type
    {
        if (projectilePrefabDictionary.TryGetValue(projectileType, out GameObject prefab))
        {
            return prefab;
        }
        else
        {
            return null; // Return null or a default prefab if the type is not found
        }
    }


    public void SetObjectActive(GameObject obj, bool isActive)
    {
        obj.SetActive(isActive);
    }


//projectile itself needs to listen to projectileEventHandler to add direction and velocity

private void OnEnable()//listens to event and passes through projectile type
    {


        ProjectileEventSystem.Instance.OnSetProjectileType += ReceiveProjectileType;
    }
    private void OnDisable()
    {
        ProjectileEventSystem.Instance.OnSetProjectileType -= ReceiveProjectileType;
    }

    public void ReceiveProjectileType(ProjectileType type)
    {
        GetPooledObject(type); //receiver sends to Get that specific object/projectile
    }




}
