using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBossChildProjectile : MonoBehaviour
{
    float damage;
    [SerializeField] float timeToSpriteSwitch = .1f;
    float knockBackDamage;
    bool hasBeenShot;

    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    AttackType attackType;
    Vector2 normalizedDirection;
    Rigidbody2D rb;
    SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void SetParameters(Vector2 direction, float velocity, float damage, float knockback, AttackType attackType)
    {

        this.damage = damage;
        normalizedDirection = direction;
        knockBackDamage = knockback;
        this.attackType = attackType;
        ApplyMovement(velocity, direction);
    }

    private void ApplyKnockback(Collider2D collision)
    {
        if (collision.TryGetComponent(out IKnockBackable knockBackable))
        {
            knockBackable.KnockBack(normalizedDirection, knockBackDamage, (int)normalizedDirection.x, (int)normalizedDirection.y);
        }
    }
    private void ApplyDamage(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(damage, attackType);
        }
    }
    void ApplyMovement(float velocity, Vector2 direction)
    {
        rb.velocity = direction * velocity;
        StartCoroutine(SwitchSpriteRoutine());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyKnockback(collision);
        ApplyDamage(collision);
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
  
}
