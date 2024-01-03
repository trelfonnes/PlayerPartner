using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryInitializer : MonoBehaviour
{
    [SerializeField] PlayerInventory playerInventory; //this is used to set the instance of PlayerInventory. It is attached to the GameManager Game Object
    [SerializeField] PlayerArtifactInventory playerArtifactInventory; //this is used to set the instance of PlayerInventory. It is attached to the GameManager Game Object

    private void Awake()
    {
        if(PlayerInventory.Instance == null)
        {
            PlayerInventory.Instance = playerInventory;
        }
        else
        {
            Debug.LogWarning("PlayerInventory already initialized. Ignoring the initialization");
        }
        if(PlayerArtifactInventory.Instance == null)
        {
            PlayerArtifactInventory.Instance = playerArtifactInventory;
        }
        else
        {
            Debug.LogWarning("PlayerArtifactInventory already initialized. Ignoring the initialization");
        }
    }
}
