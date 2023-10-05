using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemPickup : MonoBehaviour
{
    [SerializeField] KeyItem keyItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerInventory.Instance != null)
            {
                PlayerInventory.Instance.AddKeyItem(keyItem);
                Destroy(gameObject);
            }
        }
    }
}
