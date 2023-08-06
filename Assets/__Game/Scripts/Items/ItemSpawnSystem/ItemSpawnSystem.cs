using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnSystem : MonoBehaviour
{
    public ItemSpawnData itemSpawnData; // reference to the scriptable object containing spawn chances and categories of items
    IItemSpawnStrategy currentStrategy;
    public static ItemSpawnSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    public void SetInitialItemSpawnStrategy(IItemSpawnStrategy strategy)
    {
        currentStrategy = strategy;
    }

    public void SpawnItem(Transform spawnLocation)
    {
        ItemSpawnData.ItemSpawnCategory selectedCategory = DetermineSpawnCategory();
        // Add logic here to determine which category of item should be spawned
        // based on the spawn chances defined in itemSpawnData.
        // Once the category is determined, select a random item from that category
        // and instantiate it at the specified spawn location.
        currentStrategy?.SpawnItem(selectedCategory, spawnLocation);
    }

    private ItemSpawnData.ItemSpawnCategory DetermineSpawnCategory()
    {

        // Add your logic here to determine the category of the item to be spawned
        // based on the spawn chances defined in itemSpawnData.

        // For example, you could use randomization to determine the category based on spawn chances.
        // This will make it so every item has a chance to spawn with each enemy. Basic 25%, rare 10%, extra rare 1%

        //If I want to do it based on enemy strength, The enemy itself calls this system and passes in the category it has determined
        // in that case, SpawnItem above would ned to have a ItemSpawnData.ItemSpawnCategor categor as an argument
        float randomValue = UnityEngine.Random.Range(0f, 1f);
         
         if (randomValue <= itemSpawnData.extraRareItems.spawnChance)
        {
            Debug.Log("EXTRA RARE ITEM");
            return itemSpawnData.extraRareItems;
        }
        if (randomValue <= itemSpawnData.rareItems.spawnChance)
        {
            Debug.Log("RARE ITEM");
            return itemSpawnData.rareItems;
        }
        if (randomValue <= itemSpawnData.regularItems.spawnChance)
        {
            Debug.Log("REGULARiTEM");
            return itemSpawnData.regularItems;
        }
        else
        {
            Debug.Log("NO ITEM SPAWNED CHANCE");
            return itemSpawnData.noItems;
        }
    }
    public void ChangeItemSpawnStrategy(IItemSpawnStrategy strategy)
    {
        currentStrategy = strategy;
    }

}
