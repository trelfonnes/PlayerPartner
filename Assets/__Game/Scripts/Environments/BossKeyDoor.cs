using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKeyDoor : MonoBehaviour
{
    // TODO: Add functionality for loading a new scene where the boss fight takes place??
    bool doorOpened;
    [SerializeField] int costToOpen;
    [SerializeField] BoxCollider2D nonTriggerCollider;
    [SerializeField] Sprite doorOpenSprite;
    [SerializeField] Sprite doorClosedSprite;
    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {

            int keyAmount = collision.GetComponentInChildren<IKeys>().GetBossKeyAmount();

            if (keyAmount >= costToOpen && !doorOpened)
            {
                OpenDoor();
                AudioManager.Instance.PlayAudioClip("OpenDoor");

                collision.GetComponentInChildren<IKeys>().MinusBossKey(costToOpen);


            }
        }
    }

    void OpenDoor()
    {
        nonTriggerCollider.enabled = false;
        doorOpened = true;
        sr.sprite = doorOpenSprite;
    }
    public void LockDoor() // may come in handy.
    {
        nonTriggerCollider.enabled = true;
        doorOpened = false;
        sr.sprite = doorClosedSprite;
    }
}
