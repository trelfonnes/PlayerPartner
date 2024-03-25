using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemToInventory : MonoBehaviour
{
    [SerializeField]
    inventoryItems item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            item.numberHeld++;
            AudioManager.Instance.PlayAudioClip("CollectItem");

            collision.GetComponentInChildren<IInventory>().AddItemToInventory(item);
            gameObject.SetActive(false);

            
        }
    }
}
