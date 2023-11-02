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
        if (collision.CompareTag("Enemy"))
        {
            //TODO: add logic for damage and knockback and poise. 
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage, attackType);
            }
            if (collision.TryGetComponent(out IKnockBackable knockBackable))
            {
                knockBackable.KnockBack(Direction, knockBackDamage, (int)Direction.x, (int)Direction.y);
            }
            if (collision.TryGetComponent(out IPoiseDamageable poise))
            {
                poise.DamagePoise(poiseDamage);
            }
            gameObject.SetActive(false);
        }
    }


    public void Shoot(Vector2 normalizedDirection)
    {
        Direction = normalizedDirection;
        
            rb.velocity = normalizedDirection * velocity;
           
            StartCoroutine(SwitchSpriteRoutine());
            StartCoroutine(DeactivateAfterTime());

        
    }
}
