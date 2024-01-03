using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sr;

    [SerializeField] private float damage = 1; //how much damage it does
    [SerializeField] private float knockBackDamage =3; //how much knockback it gives
    [SerializeField] private float poiseDamage = 1; //how much poise damage it does
    [SerializeField] float velocity = 10f; // how fast it travels
    [SerializeField] float activeTime = 2.5f;  //how far it travels
    float timeToSpriteSwitch = .1f; 
    bool hasBeenShot;
    bool partnerProjectile;
    bool enemyProjectile;
    [SerializeField] AttackType attackType;
    private ISpecialAbility specialAbility;
    Vector2 normalizedDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
   
     void Shoot(PartnerProjectile component, Vector2 direction, float damage, float knockBackDamage)
    {
        if (!hasBeenShot)
        {
            this.damage = damage;
            this.knockBackDamage = knockBackDamage;
            enemyProjectile = false;
            partnerProjectile = true;
            float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            rb.transform.position = component.transform.position;
            transform.rotation = rotation;
           normalizedDirection = direction.normalized;

            rb.velocity = normalizedDirection * velocity;
            hasBeenShot = true;
            StartCoroutine(SwitchSpriteRoutine());
            StartCoroutine(DeactivateAfterTime());
        
        }


    }
    void EnemyShoot(EnemyProjectile component, Vector2 direction, float damage, float knockBackDamage)
    {
        if (!hasBeenShot)
        {
            this.damage = damage;
            this.knockBackDamage = knockBackDamage;
            enemyProjectile = true;
            partnerProjectile = false;
            float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            rb.transform.position = component.transform.position;
            transform.rotation = rotation;
            normalizedDirection = direction.normalized;

            rb.velocity = normalizedDirection * velocity;
            hasBeenShot = true;
            StartCoroutine(SwitchSpriteRoutine());
            StartCoroutine(DeactivateAfterTime());
        }
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
        gameObject.SetActive(false);
    }

  private void OnTriggerEnter2D(Collider2D collision)
{
        SetSpecialAbility(attackType, collision);
        if(partnerProjectile && collision.CompareTag("Enemy"))
        {
            TakeCareOfCollision(collision);
        }
        

        if (enemyProjectile && collision.CompareTag("Partner") || enemyProjectile && collision.CompareTag("Player"))
    {
            TakeCareOfCollision(collision);
    }
    
}
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
            knockBackable.KnockBack(normalizedDirection, knockBackDamage, (int)normalizedDirection.x, (int)normalizedDirection.y);
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
        if(type == AttackType.Fire)
        {
            specialAbility = new IProjIgniteSA();
            specialAbility.ExecuteSpecialAbility(collider);
        }
        if(type == AttackType.Water)
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
    private void OnEnable()//subscribe so same prefab can be used for both enemy and partner
    {
        ProjectileEventSystem.Instance.OnPartnerDirectionSet += Shoot;
        ProjectileEventSystem.Instance.OnEnemyDirectionSet += EnemyShoot;
    }


    private void OnDisable()
    {
        ProjectileEventSystem.Instance.OnPartnerDirectionSet -= Shoot;
        ProjectileEventSystem.Instance.OnEnemyDirectionSet -= EnemyShoot;

    }

}
