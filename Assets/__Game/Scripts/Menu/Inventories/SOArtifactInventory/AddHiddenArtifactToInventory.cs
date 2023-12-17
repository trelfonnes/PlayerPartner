using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHiddenArtifactToInventory : MonoBehaviour, IInteractable
{
    [SerializeField] ArtifactInventoryItems artifactSO;
    [SerializeField] ArtifactInventoryManager manager;
    public void Interact()
    {
        if (manager != null)
        {
            manager.AddArtifactToInventory(artifactSO);
            gameObject.SetActive(false);

        }
        else
        {
            Debug.LogError("no Manager reference on hidden item");
        }
    }
}
