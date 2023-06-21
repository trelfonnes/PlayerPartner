using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerAmmo : MonoBehaviour
{
    [SerializeField] int amount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<ISpecialPower>().IncreaseSP(amount);
            gameObject.SetActive(false);
        }

    }
}
