using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddKeys : MonoBehaviour
{
    [SerializeField] int numberOfKeys = 1;
    [SerializeField] bool isBossKey;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            if (isBossKey)
            {
                collision.GetComponentInChildren<IKeys>().AddBossKey(numberOfKeys);
                AudioManager.Instance.PlayAudioClip("BossKey");

                gameObject.SetActive(false);

            }
            else
            {
                collision.GetComponentInChildren<IKeys>().AddKey(numberOfKeys);
                AudioManager.Instance.PlayAudioClip("Key");

                gameObject.SetActive(false);

            }
        }
    }
}
