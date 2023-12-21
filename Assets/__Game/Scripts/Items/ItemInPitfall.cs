using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInPitfall : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Vector2 startingPosition;
    [SerializeField] float timeToRespawn;
    Sprite startingSprite;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
      startingSprite = sr.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rb.velocity == Vector2.zero)
        {
            if (collision.CompareTag("Pitfall"))
            {
                sr.sprite = null;
                RespawnItem();
            }            
                    
        } 
    
    
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (rb.velocity == Vector2.zero)
        {
            if (collision.CompareTag("Pitfall"))
            {
                if (gameObject.CompareTag("Bomb"))
                {
                    TimerBomb bomb = GetComponentInParent<TimerBomb>();
                    bomb.hasBeenShot = false;
                    gameObject.SetActive(false);

                }
                else
                {
                    sr.sprite = null;
                    StartCoroutine(RespawnItem());
                }
            }

        }
    }

    IEnumerator RespawnItem()
    {
        yield return new WaitForSeconds(timeToRespawn);

        transform.position = startingPosition;
        sr.sprite = startingSprite;
    }





}
