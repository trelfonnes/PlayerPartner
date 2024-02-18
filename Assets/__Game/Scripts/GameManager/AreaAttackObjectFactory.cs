using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttackObjectFactory : MonoBehaviour
{
    private static AreaAttackObjectFactory instance;
    public static AreaAttackObjectFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AreaAttackObjectFactory>();
                if (instance == null)
                {
                    GameObject go = new GameObject("AreaAttackObjectFactory");
                    instance = go.AddComponent<AreaAttackObjectFactory>();
                }
            }
            return instance;
        }
    }
    private Dictionary<AttackType, List<AreaAttackObject>> pooledObjects;
    private Dictionary<AttackType, AreaAttackObject> prefabDictionary; // This is what you need

    // Reference to the AreaAttackObject prefabs add for more then intialize on awake
    [SerializeField] AreaAttackObject firePrefab;
    private void Awake()
    {
        // Initialize pooledObjects with the appropriate mappings
        pooledObjects = new Dictionary<AttackType, List<AreaAttackObject>>();
        prefabDictionary = new Dictionary<AttackType, AreaAttackObject>
        {
            {AttackType.Fire, firePrefab}
            //add other mappings as needed
        };

    }
    public AreaAttackObject CreateAreaAttackObject(AttackType type)
    {
        // Check if there is a pool for the specified AttackType
        if (!pooledObjects.ContainsKey(type))
        {
            pooledObjects[type] = new List<AreaAttackObject>();
        }

        List<AreaAttackObject> pool = pooledObjects[type];

        // Check if there are any inactive objects in the pool
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                // Reuse the existing object
                AreaAttackObject areaAttackInstance = pool[i];
                areaAttackInstance.gameObject.SetActive(true);
                return areaAttackInstance;
            }

        }
            // If no inactive objects are available, instantiate a new one
                AreaAttackObject newAreaAttackInstance = Instantiate(prefabDictionary[type]);
                pool.Add(newAreaAttackInstance);
                newAreaAttackInstance.gameObject.SetActive(true);
                return newAreaAttackInstance;

           
        
        Debug.LogError($"Prefab for AttackType {type} not found.");
        return null;
    
    }




    public void ResetPooledObjects()
    {
        foreach (var kvp in pooledObjects)
        {
            foreach (var obj in kvp.Value)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}
