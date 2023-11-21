using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveAndRestore : MonoBehaviour, IInteractable
{
    BoxCollider2D triggerCollider;
    public void Interact()
    {
        triggerCollider.enabled = true;

    }
    private void Start()
    {
        triggerCollider = GetComponent<BoxCollider2D>();
        triggerCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.TryGetComponent(out IReviveAndRestore RAndR))
        {
            if (!collision.CompareTag("Enemy"))
            {

                RAndR.ReviveAndRestore();
            }
        }
        triggerCollider.enabled = false;

    }

}
