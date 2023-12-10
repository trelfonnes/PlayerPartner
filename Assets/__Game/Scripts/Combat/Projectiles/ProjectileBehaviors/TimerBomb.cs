using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBomb : MonoBehaviour, IKnockBackable
{
    private float knockbackDuration = 0.2f;
    private float knockbackTimer = 0f;
    [SerializeField] private float decayFactor = 0.95f;
    private Vector2 knockbackDirection;
    private float knockbackSpeed;
    Rigidbody2D rb;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sr;
    
    [SerializeField] AttackType attackType;
    [SerializeField] private float damage = 1; 
    [SerializeField] private float knockBackDamage = 3; 
    [SerializeField] private float poiseDamage = 1;
    [SerializeField] float setTime = 2.5f;
    [SerializeField] StoredParticles storedParticles;
    float timeToSpriteSwitch = .2f;
   [SerializeField] private float radius = 5f;
    bool hasBeenShot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Shoot(Projectile component, Vector2 direction)
    {
        if (!hasBeenShot)
        {
            rb.velocity = Vector2.zero;
            // Offset the bomb's position by 1 unit in the specified direction
            Vector2 offset = direction.normalized; // Ensure it's a unit vector
            Debug.Log(offset + "This is the bombs offset");

            transform.position = (Vector2)component.transform.position + offset;

            StartCoroutine(ExplodeCoroutine(direction));
            StartCoroutine(SwitchSpriteRoutine());
            hasBeenShot = true;

            // bomb needs to count down from set time
            // then check if anything is in its trigger collider and apply damage etc. 

        }

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
        ExplodeParticles();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var item in colliders)
        {

            // Check if the collider has a script or component to take damage and apply knockback.
            if (item.TryGetComponent(out IKnockBackable knockBackable))
            {
                Debug.Log("DamagingKnockBack");
                if (!item.CompareTag("Bomb"))
                {
                    knockBackable.KnockBack(direction, knockBackDamage, (int)direction.x, (int)direction.y);
                }
            }

            if (item.TryGetComponent(out IPoiseDamageable poise))
            {
                Debug.Log("DamagingPoise");
                poise.DamagePoise(poiseDamage);
            }
            if (item.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log(item.gameObject.name);

                Debug.Log("DamagingHealth");

                damageable.Damage(damage, attackType);
            }
            if(item.TryGetComponent(out IBombable bombable))
            {
                bombable.Explode();
            }
            hasBeenShot = false;
            gameObject.SetActive(false);
           
        }
    }
    private void ExplodeParticles()
    {
        GameObject bombEffect = Instantiate(storedParticles.GetParticlePrefab(ParticleType.BombExplode), transform.position, Quaternion.identity);
        
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

    private void Update()
    {
        if (knockbackTimer < knockbackDuration)
        {
            // Set the velocity based on the knockback direction and speed
            rb.velocity = knockbackDirection * (knockbackSpeed *2);

            // Decrease the speed with the decay factor
            knockbackSpeed *= decayFactor;

            // Increase the timer
            knockbackTimer += Time.deltaTime;
        }
        else
        {
            // Stop the bomb after the knockback duration
            rb.velocity = Vector2.zero;
        }
    }

    public void KnockBack(Vector2 angle, float initialSpeed, int directionX, int directionY)
    {
        knockbackSpeed = initialSpeed;
        knockbackDirection = new Vector2(directionX, directionY).normalized;

        // Reset the timer
        knockbackTimer = 0f;
    }
}

