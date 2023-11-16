using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    [SerializeField] float touchDamageAmount;
    [SerializeField] float touchKnockBackStrength;
    [SerializeField] float touchPoiseDamage;
    [SerializeField] AttackType touchDamageType;
    int directionX;
    int directionY;
    private float lastTouchDamageTime;
    [SerializeField] float touchDamageCooldown = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time - lastTouchDamageTime > touchDamageCooldown)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Partner"))
            {
                ApplyDamage(collision);
                lastTouchDamageTime = Time.time;
                ApplyKnockback(collision);
                ApplyPoiseDamage(collision);

            }
        }
    }
    private void ApplyDamage(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(touchDamageAmount, touchDamageType);
        }
    }

    private void ApplyKnockback(Collider2D collision)
    {
        int randomValueX = Random.Range(-1, 2);
        directionX = randomValueX;
        int RandomValueY = Random.Range(-1, 2);
        directionY = RandomValueY;

        Vector2 randomDirection = new Vector2(directionX, directionX);
        if (collision.TryGetComponent(out IKnockBackable knockBackable))
        {
            knockBackable.KnockBack(randomDirection, touchKnockBackStrength, directionX, directionY);
        }
    }

    private void ApplyPoiseDamage(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPoiseDamageable poise))
        {
            poise.DamagePoise(touchPoiseDamage);
        }
    }
}
