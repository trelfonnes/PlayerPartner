using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBomb : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sr;
  

    [SerializeField] private float damage = 1; 
    [SerializeField] private float knockBackDamage = 3; 
    [SerializeField] private float poiseDamage = 1;
    [SerializeField] float setTime = 2.5f;

    float timeToSpriteSwitch = .2f;
   [SerializeField] private float radius = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Shoot(Projectile component, Vector2 direction)
    {
        transform.position = component.transform.position; //bomb just placed at location like zelda
        StartCoroutine(ExplodeCoroutine(direction));
        StartCoroutine(SwitchSpriteRoutine());


        // bomb needs to count down from set time
        // then check if anything is in its trigger collider and apply damage etc. 



    }
    IEnumerator ExplodeCoroutine(Vector2 direction)
    {
        yield return new WaitForSeconds(setTime);
        Explode(direction);
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

    private void Explode(Vector2 direction)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider has a script or component to take damage and apply knockback.
            if(collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
            if(collider.TryGetComponent(out IKnockBackable knockBackable))
            {
                knockBackable.KnockBack(direction, knockBackDamage, (int)direction.x, (int)direction.y);
            }
            if(collider.TryGetComponent(out IPoise poise))
            {
                poise.DecreasePoise(poiseDamage);
            }
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        ProjectileEventSystem.Instance.OnPlayerDirectionSet += Shoot;
    }

    private void OnDisable()
    {
        ProjectileEventSystem.Instance.OnPlayerDirectionSet -= Shoot;
    }

    private void OnDrawGizmos()
    {
        // Draw a wire sphere to visualize the explosion radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
