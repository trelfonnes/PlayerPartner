using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickHealingItem : MonoBehaviour
{
    [SerializeField] private bool SetSickTo = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TODO: change add to inventory if "Player" or change to "Partner"
            collision.GetComponentInChildren<ISick>().SickONandOFF(SetSickTo);
            gameObject.SetActive(false);
        }
    }
}
