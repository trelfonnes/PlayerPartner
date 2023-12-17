using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddArtifactToInventory : MonoBehaviour
{
    [SerializeField] ArtifactInventoryItems artifactSO;
    [SerializeField] ArtifactInventoryManager manager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            manager.AddArtifactToInventory(artifactSO);
            gameObject.SetActive(false);
        }
    }
}
