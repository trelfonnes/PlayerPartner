using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorConditional : MonoBehaviour
{
    BoxCollider2D doorCollider;
    [SerializeField] Sprite doorOpen;
    [SerializeField] Sprite doorClosed;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        doorCollider.enabled = true;
    }


   public void OpenDoor()
    {
        doorCollider.enabled = false;
        spriteRenderer.sprite = doorOpen;
    }

    public void CloseDoor() 
    {
        doorCollider.enabled = true;
        spriteRenderer.sprite = doorClosed;
    }





}
