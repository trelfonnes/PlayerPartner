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
    [SerializeField] AttackType attackType;

    Vector2 normalizedDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
   
     void Shoot(PartnerProjectile component, Vector2 direction)
    {
        if (!hasBeenShot)
        {
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
        if(collision.CompareTag("Enemy"))
        {
            //TODO: add logic for damage and knockback and poise. 
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage, attackType);
            }
            if(collision.TryGetComponent(out IKnockBackable knockBackable))
            {
                knockBackable.KnockBack(normalizedDirection, knockBackDamage, (int)normalizedDirection.x, (int)normalizedDirection.y);
            }
            if(collision.TryGetComponent(out IPoiseDamageable poise))
            {
                poise.DamagePoise(poiseDamage);
            }
            hasBeenShot = false;
            gameObject.SetActive(false);
        }
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
