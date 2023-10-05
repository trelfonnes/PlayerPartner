using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItemSpawnStrategy : IItemSpawnStrategy
{
    private ItemsObjectPool objectPool;

    public RareItemSpawnStrategy(ItemsObjectPool pool)
    {
        objectPool = pool;
    }
    public void SpawnItem(ItemSpawnData.ItemSpawnCategory category, Transform spawnLocation)
    {
        int itemToReturn = Random.Range(0, category.items.Length);
        var poolItem = objectPool.rareItems.Find(item => item.prefab == category.items[itemToReturn]); // Adjust this line based on your needs.

        if (poolItem != null)
        {
            GameObject itemInstance = objectPool.GetPooledObject(poolItem.prefab, spawnLocation.position, Quaternion.identity);
            if (itemInstance != null)
            {
                // Customize any other settings or behavior for the spawned item here.
            }
        }
    }
}
