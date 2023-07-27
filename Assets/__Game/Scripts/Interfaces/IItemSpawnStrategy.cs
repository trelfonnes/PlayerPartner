using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemSpawnStrategy 
{
    void SpawnItem(ItemSpawnData.ItemSpawnCategory category, Transform spawnLocation);
    
}
