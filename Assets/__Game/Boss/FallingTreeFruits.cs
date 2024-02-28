using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTreeFruits : MonoBehaviour
{
    [SerializeField] EnemyStatEvents forestBossStatEvents;
    [SerializeField] List<GameObject> fruitsToDrop;
    [SerializeField] List<Transform> positionsToDrop;

    private void OnEnable()
    {
        forestBossStatEvents.onHealthLow += DropFruits;
    }
    private void OnDisable()
    {
        forestBossStatEvents.onHealthLow -= DropFruits;

    }
    void DropFruits()
    {
        forestBossStatEvents.onHealthLow -= DropFruits;

        foreach (GameObject fruitPrefab in fruitsToDrop)
        {
            // Get a random index for the position to drop
            int randomIndex = Random.Range(0, positionsToDrop.Count);

            // Instantiate the fruit prefab at the random position
            Instantiate(fruitPrefab, positionsToDrop[randomIndex].position, Quaternion.identity);
        }
    }
    
}
