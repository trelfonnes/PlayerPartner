using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthIncrease : MonoBehaviour
{
    [SerializeField] private float amountToIncrease = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Partner"))
        {
            collision.GetComponentInChildren<IHealthChange>().IncreaseMaxHealth(amountToIncrease);
            gameObject.SetActive(false);
        }
    }
}
