using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class PoolItem
    {
        public GameObject prefab;
        public int poolSize;
    }
    public List<PoolItem> regularItems = new List<PoolItem>();
    public List<PoolItem> rareItems = new List<PoolItem>();
    public List<PoolItem> extraRareItems = new List<PoolItem>();
    private Dictionary<GameObject, Queue<GameObject>> pooledObjects 
        = new Dictionary<GameObject, Queue<GameObject>>();

    private void Start()
    {
        CreatePool(regularItems);
        CreatePool(rareItems);
        CreatePool(extraRareItems);
    }
    private void CreatePool(List<PoolItem> poolItems)
    {
        foreach (PoolItem poolItem in poolItems)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < poolItem.poolSize; i++)
            {
                GameObject newObject = Instantiate(poolItem.prefab);
                newObject.SetActive(false);
                newObject.transform.SetParent(transform);
                objectPool.Enqueue(newObject);
            }

            pooledObjects.Add(poolItem.prefab, objectPool);
        }
    }
    public GameObject GetPooledObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (pooledObjects.TryGetValue(prefab, out Queue<GameObject> objectPool) && objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }

        // Increase pool size or handle pool growth dynamically if necessary.
        Debug.LogWarning("Object pool is empty!");
        return null;
    }

  


}

