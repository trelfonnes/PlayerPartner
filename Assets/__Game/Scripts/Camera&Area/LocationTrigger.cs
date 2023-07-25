using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    [SerializeField] GameEvent eventToTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          
            if (eventToTrigger.CheckCondition(PlayerInventory.Instance))
            {
                eventToTrigger.Trigger();
            }
            else
            {
                Debug.Log("You don't have the needed key item to enter");
            }
        }
    }



}
