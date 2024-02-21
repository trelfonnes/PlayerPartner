using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonItemCollected : MonoBehaviour
{
    [SerializeField] private string itemName;  // Serialized field for the item's unique identifier

    private DungeonManager dungeonManager;

    private void Start()
    {
        dungeonManager = FindObjectOfType<DungeonManager>();
        if (dungeonManager.IsItemCollected(itemName))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Collect the item by passing its unique identifier to the Dungeon Manager
            dungeonManager.CollectItem(itemName);

            // Additional logic for the collectible item, such as deactivating the object
            gameObject.SetActive(false);
        }
    }
}