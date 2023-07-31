using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularItemSpawnStrategy : IItemSpawnStrategy
{
    private ItemsObjectPool objectPool;

    public RegularItemSpawnStrategy(ItemsObjectPool pool) //constructors that allow them to receive reference to objectPool from gameManager
    {
        objectPool = pool;
    }
    public void SpawnItem(ItemSpawnData.ItemSpawnCategory category, Transform spawnLocation)
    {
        int itemToReturn = Random.Range(0, category.items.Length);
        var poolItem = objectPool.regularItems.Find(item => item.prefab == category.items[itemToReturn]); // Adjust this line based on your needs.
        Debug.Log(poolItem );
        Debug.Log(itemToReturn + "itemToReturn");
        Debug.Log(objectPool + "object pool");


        if (poolItem != null)
        {
            Debug.Log("poolItem is not null");

            GameObject itemInstance = objectPool.GetPooledObject(poolItem.prefab, spawnLocation.position, Quaternion.identity);
            if (itemInstance != null)
            {
                Debug.Log("item instance is not null");

                // Customize any other settings or behavior for the spawned item here.
            }
        }
    }
}
