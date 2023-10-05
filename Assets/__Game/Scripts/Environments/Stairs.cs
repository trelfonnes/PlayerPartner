using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float stairsDrag;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger || collision.CompareTag("Partner") && !collision.isTrigger)
        {
            rb = collision.GetComponent<Rigidbody2D>();
            rb.drag = stairsDrag;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            rb = collision.GetComponent<Rigidbody2D>();
            rb.drag = 0;
        }
        if (collision.CompareTag("Partner") && !collision.isTrigger)
        {
            rb = collision.GetComponent<Rigidbody2D>();
            rb.drag = 0;
        }
    }
}
