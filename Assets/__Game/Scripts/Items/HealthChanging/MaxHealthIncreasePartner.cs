using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthIncreasePartner : MonoBehaviour
{
    [SerializeField] private float amountToIncrease = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner") && !collision.isTrigger)
        {
            collision.GetComponentInChildren<IHealthChange>().IncreaseMaxHealth(amountToIncrease);
            gameObject.SetActive(false);
        }
    }
}
