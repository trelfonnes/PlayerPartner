using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredHealingItem : MonoBehaviour
{
    [SerializeField]
    inventoryItems bandAid;

    private bool setInjuredTo = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //TODO: change add to inventory if "Player" or change to "Partner"
            bandAid.numberHeld ++;
            gameObject.SetActive(false);
            
        }
    }
}
