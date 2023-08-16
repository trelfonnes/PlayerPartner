using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponToPickup : MonoBehaviour
{
    [SerializeField] WeaponDataSO thisWeaponData;
    [SerializeField] WeaponInventoryManager weaponManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            weaponManager.AddToWeaponInventory(thisWeaponData);
            gameObject.SetActive(false);
        }
    }
}
