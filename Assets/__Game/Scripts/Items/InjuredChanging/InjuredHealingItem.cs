using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredHealingItem : MonoBehaviour
{
    [SerializeField] private bool setInjuredTo = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //TODO: change add to inventory if "Player" or change to "Partner"

            collision.GetComponentInChildren<IInjured>().InjuredONandOFF(setInjuredTo);
            gameObject.SetActive(false);
        }
    }
}
