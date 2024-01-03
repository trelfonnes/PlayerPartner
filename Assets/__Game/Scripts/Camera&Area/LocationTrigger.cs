using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    public EnterKeyItemEventSO eventToTrigger;
    public PlayerArtifactInventory artifactInventory;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          
            if (eventToTrigger.CheckCondition(artifactInventory))
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
