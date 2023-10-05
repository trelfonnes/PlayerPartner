using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
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
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {

           int keyAmount = collision.GetComponentInChildren<IKeys>().GetKeyAmount();

            if(keyAmount >= costToOpen && !doorOpened)
            {
                OpenDoor();
                collision.GetComponentInChildren<IKeys>().MinusKey(costToOpen);
                

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
        doorOpened = false;
        nonTriggerCollider.enabled = true;
        sr.sprite = doorClosedSprite;
    }
}
