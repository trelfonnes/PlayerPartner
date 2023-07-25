using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryInitializer : MonoBehaviour
{
    [SerializeField] PlayerInventory playerInventory;

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
    }
}
