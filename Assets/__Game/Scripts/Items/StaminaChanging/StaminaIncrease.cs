using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaIncrease : MonoBehaviour
{
    [SerializeField] private float amountToIncrease = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner") && !collision.isTrigger)
        {
            collision.GetComponentInChildren<IStaminaChange>().IncreaseStamina(amountToIncrease);
            gameObject.SetActive(false);
            
        }
    }
}
