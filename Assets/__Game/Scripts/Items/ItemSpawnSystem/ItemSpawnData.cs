using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSpawnData", menuName = "ItemSpawn/Item Spawn Data")]

public class ItemSpawnData : ScriptableObject
{
    [System.Serializable]
    public struct ItemSpawnCategory
    {
        public string categoryName; // i.e. regular items etc.
        public float spawnChance;  // i.e. 25%
        public GameObject[] items; // array of items that fit these categories and spawn chances
    }


    public ItemSpawnCategory regularItems;
    public ItemSpawnCategory rareItems;
    public ItemSpawnCategory extraRareItems;
    public ItemSpawnCategory noItems;

}
