using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D circleCollider;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    
    SpriteRenderer sr;
    [SerializeField] float velocity = 10f;
    float timeToSpriteSwitch = .2f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
   
    protected virtual void Shoot(PartnerProjectile component, Vector2 direction)
    {
        rb.transform.position = component.transform.position;
        Debug.Log(direction);
        Vector2 normalizedDirection = direction.normalized;

        rb.velocity = normalizedDirection * velocity;

        StartCoroutine(SwitchSpriteRoutine());

    }
    IEnumerator SwitchSpriteRoutine()
    {
        while (true)
        {
            // Loop through the list of sprites
            for (int i = 0; i < sprites.Count; i++)
            {
                sr.sprite = sprites[i];

                // Wait for the specified duration
                yield return new WaitForSeconds(timeToSpriteSwitch);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //apply damage and knockback
        // return to pool
    }

    private void OnEnable()
    {
        ProjectileEventSystem.Instance.OnPartnerDirectionSet += Shoot;
    }


    private void OnDisable()
    {
        ProjectileEventSystem.Instance.OnPartnerDirectionSet -= Shoot;
    }

}
