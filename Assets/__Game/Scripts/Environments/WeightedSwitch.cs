using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedSwitch : MonoBehaviour
{
    [SerializeField] Sprite switchPressed;
    [SerializeField] Sprite switchUnPressed;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = switchUnPressed;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Boulder"))
        {
            spriteRenderer.sprite = switchPressed;
            Debug.Log("Open a gate or something");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boulder"))
        {
            spriteRenderer.sprite = switchUnPressed;
            Debug.Log("Gate is closed");
        }
    }
}
