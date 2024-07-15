using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBytes : MonoBehaviour
{

    [SerializeField] int amount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            collision.GetComponentInChildren<IBytes>().IncreaseBytes(amount);
            AudioManager.Instance.PlayAudioClip("CollectByte");

            gameObject.SetActive(false);

        }

    }
}
