using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponToPickup : MonoBehaviour
{
    [SerializeField] WeaponInventoryManager weaponManager;
    [SerializeField] bool playerWeapon;
    [SerializeField] WeaponInventoryItemSO thisItemsData;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (playerWeapon)
            {
              weaponManager.AddToPlayerWeaponInventory(thisItemsData);
                gameObject.SetActive(false);
            }
            else
            {
                weaponManager.AddToPartnerWeaponInventory(thisItemsData);
                gameObject.SetActive(false);
            }
            
        
        }

    }
}
