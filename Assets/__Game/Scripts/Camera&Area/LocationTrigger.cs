using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    public EnterKeyItemEventSO eventToTrigger;
    public PlayerArtifactInventory artifactInventory;
    [SerializeField] GameObject ObjectToInfluence; //drag and drop gameObject into inspector here.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          
            if (eventToTrigger.CheckCondition(artifactInventory))
            {
                Debug.Log(eventToTrigger.ActionMessage);
                ObjectToInfluence.GetComponent<KeyItemTriggerManager>().TriggerKeyItemEvent();
            }
            else
            {
                Debug.Log("You don't have the needed key item to enter");
            }
        }
    }



}
