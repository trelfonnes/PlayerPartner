using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sr;

    [SerializeField] private float damage = 1; //how much damage it does
    [SerializeField] private float knockBackDamage = 3; //how much knockback it gives
    [SerializeField] private float poiseDamage = 1; //how much poise damage it does
    [SerializeField] float velocity = 10f; // how fast it travels
    [SerializeField] float activeTime = 2.5f;
    [SerializeField] private float chargedDamage = 1; //how much damage it does
    [SerializeField] private float chargedKnockBackDamage = 3; //how much knockback it gives
    [SerializeField] private float chargedPoiseDamage = 1; //how much poise damage it does
    [SerializeField] float chargedVelocity = 10f; // how fast it travels

    float timeToSpriteSwitch = .1f;
    float scaleFactor = 1.5f;

    bool hasBeenShot;
    bool isCharged;

    Vector2 normalizedDirection;

    Vector3 baseScale;
    Vector3 chargedScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        baseScale = new Vector3(1, 1, 1);
    }

    void Shoot(PartnerProjectile component, Vector2 direction)
    {
        chargedScale = baseScale * scaleFactor;

        if (!hasBeenShot)
        {
            float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            transform.position = component.transform.position;
            transform.rotation = rotation;
            normalizedDirection = direction.normalized;
            if (isCharged)
            {
                transform.localScale = chargedScale;
                rb.velocity = normalizedDirection * chargedVelocity;
            }
            else 
            {
                transform.localScale = baseScale;
               rb.velocity = normalizedDirection * velocity;
            }
            hasBeenShot = true;
            StartCoroutine(SwitchSpriteRoutine());
            StartCoroutine(DeactivateAfterTime());
        }


    }
    private void SetCharged(bool charged)
    {
        isCharged = charged;
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

    IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(activeTime);
        hasBeenShot = false;
        isCharged = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //TODO: add logic for damage and knockback and poise. 
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                if (isCharged)
                {
                    damageable.Damage(chargedDamage);
                }
                else
                damageable.Damage(damage);
            }
            if (collision.TryGetComponent(out IKnockBackable knockBackable))
            {
                if (isCharged)
                {
                    knockBackable.KnockBack(normalizedDirection, chargedKnockBackDamage, (int)normalizedDirection.x, (int)normalizedDirection.y);
                }
                else
                    knockBackable.KnockBack(normalizedDirection, knockBackDamage, (int)normalizedDirection.x, (int)normalizedDirection.y);
            }
            if (collision.TryGetComponent(out IPoiseDamageable poise))
            {
                if (isCharged)
                {
                    poise.DamagePoise(chargedPoiseDamage);
                }
                else
                    poise.DamagePoise(poiseDamage);
            }
            hasBeenShot = false;
            isCharged = false;
            transform.localScale = baseScale;
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ProjectileEventSystem.Instance.OnPartnerDirectionSet += Shoot;
        ProjectileEventSystem.Instance.OnPartnerShotIsCharged += SetCharged;
    }

    private void OnDisable()
    {
        ProjectileEventSystem.Instance.OnPartnerDirectionSet -= Shoot;
        ProjectileEventSystem.Instance.OnPartnerShotIsCharged -= SetCharged;
    }


}
