using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : MonoBehaviour
{
    [SerializeField] private float amountToIncrease = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger || collision.CompareTag("Partner") && !collision.isTrigger) 
        {
            collision.GetComponentInChildren<IHealthChange>().IncreaseHealth(amountToIncrease);
            gameObject.SetActive(false);
        }
    }
}
