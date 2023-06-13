using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpecialAmmo : MonoBehaviour
{
    [SerializeField] int amount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Partner")
        {
            collision.GetComponentInChildren<ISpecialPower>().IncreaseSP(amount);
            gameObject.SetActive(false);

        }

    }
}
