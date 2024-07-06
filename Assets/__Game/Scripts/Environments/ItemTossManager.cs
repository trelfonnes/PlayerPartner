using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTossManager : MonoBehaviour
{
    [SerializeField] WaterBridge waterBridge;

    int itemsCollected;
    [SerializeField] int itemsNeededToEnter;
    private void Start()
    {
        itemsCollected = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Carryable") && !collision.isTrigger) //Carryable items have two colliders this prevents from being counted twice
        {
            itemsCollected++;
            Debug.Log(collision.name + "Inside the collision tag pass");
            collision.gameObject.SetActive(false);
            //play a plopping in water sound
            //play a splash animation where the item is set to innactive
            if(itemsCollected >= itemsNeededToEnter)
            {
                ActivateObject();
            }

        }
    }
    void ActivateObject()
    {
        if (waterBridge)
        {
            waterBridge.Execute();
        }
    }



}
