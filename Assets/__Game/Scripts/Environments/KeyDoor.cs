using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
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
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {

           int keyAmount = collision.GetComponentInChildren<IKeys>().GetKeyAmount();

            if(keyAmount >= costToOpen)
            {
                OpenDoor();
                collision.GetComponentInChildren<IKeys>().MinusKey(costToOpen);
                

            }
        }
    }

    void OpenDoor()
    {
        nonTriggerCollider.enabled = false;
        sr.sprite = doorOpenSprite;
    }
    public void LockDoor() // may come in handy.
    {
        nonTriggerCollider.enabled = true;
        sr.sprite = doorClosedSprite;
    }
}
