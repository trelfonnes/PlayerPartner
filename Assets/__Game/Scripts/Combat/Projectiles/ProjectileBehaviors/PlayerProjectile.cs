using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] AttackType attackType;
    [SerializeField] private float damage = 1; //how much damage it does
    [SerializeField] private float knockBackDamage = 3; //how much knockback it gives
    [SerializeField] private float poiseDamage = 1; //how much poise damage it does
    [SerializeField] float velocity = 10f; // how fast it travels
    [SerializeField] float activeTime = 2.5f;  //how far it travels
    bool hasBeenShot;

    Vector2 normalizedDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Shoot(Projectile component, Vector2 direction)
    {
        if (!hasBeenShot)
        {
            AudioManager.Instance.PlayAudioClip("Dart");
            float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            rb.transform.position = component.transform.position;
            transform.rotation = rotation;
            normalizedDirection = direction.normalized;

            rb.velocity = normalizedDirection * velocity;
            hasBeenShot = true;
            StartCoroutine(DeactivateAfterTime());

        }
    }
    IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(activeTime);
        hasBeenShot = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //TODO: add logic for damage and knockback and poise. 
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage, attackType);
            }
            if (collision.TryGetComponent(out IKnockBackable knockBackable))
            {
                knockBackable.KnockBack(normalizedDirection, knockBackDamage, (int)normalizedDirection.x, (int)normalizedDirection.y);
            }
            if (collision.TryGetComponent(out IPoiseDamageable poise))
            {
                poise.DamagePoise(poiseDamage);
            }
            if(collision.TryGetComponent( out IDartTarget target))
            {
                target.BullsEye();
            }
            hasBeenShot = false;
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
}
