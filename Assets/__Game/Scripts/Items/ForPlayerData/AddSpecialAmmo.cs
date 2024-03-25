using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSpecialAmmo : MonoBehaviour
{
    [SerializeField] int amount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner") && !collision.isTrigger)
        {
            collision.GetComponentInChildren<ISpecialPower>().IncreaseSP(amount);
            AudioManager.Instance.PlayAudioClip("CollectAmmo");

            gameObject.SetActive(false);

        }

    }
}
