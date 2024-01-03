using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemPickup : MonoBehaviour
{
    [SerializeField] KeyItem keyItemActionCheck;
    [SerializeField] PlayerArtifactInventory artifactInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (artifactInventory)
            {
                artifactInventory.AddKeyItem(keyItemActionCheck);
                Destroy(gameObject);
            }
        }
    }
}
