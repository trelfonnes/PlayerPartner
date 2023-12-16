using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadProjectileRight : MonoBehaviour
{
    CircleCollider2D circleCollider;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sr;
    [SerializeField] AttackType attackType;
    [SerializeField] private float damage = 1; //how much damage it does
    [SerializeField] private float knockBackDamage = 3; //how much knockback it gives
    [SerializeField] private float poiseDamage = 1; //how much poise damage it does
    [SerializeField] float velocity = 10f; // how fast it travels
    [SerializeField] float activeTime = 2.5f;  //how far it travels
    float timeToSpriteSwitch = .2f;
    bool hasBeenShot;
    Rigidbody2D rb;
    Vector2 Direction;
    private ISpecialAbility specialAbility;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        SetSpecialAbility(attackType, collision);
        if (collision.CompareTag("Enemy"))
        {
            TakeCareOfCollision(collision);
        }



    }

    //TODO: add logic for damage and knockback and poise. 

    void TakeCareOfCollision(Collider2D collision)
    {
        SetSpecialAbility(attackType, collision);
        ApplyDamage(collision);
        ApplyKnockback(collision);
        ApplyPoiseDamage(collision);

        hasBeenShot = false;
        gameObject.SetActive(false);
    }
    private void ApplyDamage(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(damage, attackType);
        }
    }

    private void ApplyKnockback(Collider2D collision)
    {
        if (collision.TryGetComponent(out IKnockBackable knockBackable))
        {
            knockBackable.KnockBack(Direction, knockBackDamage, (int)Direction.x, (int)Direction.y);
        }
    }

    private void ApplyPoiseDamage(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPoiseDamageable poise))
        {
            poise.DamagePoise(poiseDamage);
        }
    }
    void SetSpecialAbility(AttackType type, Collider2D collider)
    {
        if (type == AttackType.Fire)
        {
            specialAbility = new IProjIgniteSA();
            specialAbility.ExecuteSpecialAbility(collider);
        }
        if (type == AttackType.Water)
        {
            specialAbility = new IProjExtinguish();
            specialAbility.ExecuteSpecialAbility(collider);

        }
        if (type == AttackType.Poison)
        {
            specialAbility = new IProjCorrode();
            specialAbility.ExecuteSpecialAbility(collider);

        }
        if (type == AttackType.Electric)
        {
            specialAbility = new IProjPowerOn();
            specialAbility.ExecuteSpecialAbility(collider);

        }
    }

    public void Shoot(Vector2 normalizedDirection)
    {
        Direction = normalizedDirection;
        
           // rb.velocity = normalizedDirection * velocity;
           
            StartCoroutine(SwitchSpriteRoutine());
            StartCoroutine(DeactivateAfterTime());

        
    }
}
