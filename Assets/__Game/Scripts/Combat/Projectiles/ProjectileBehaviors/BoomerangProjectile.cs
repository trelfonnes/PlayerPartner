using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : MonoBehaviour, IInteractable
{
    [SerializeField] List<Sprite> boomerangSprites = new List<Sprite>();
    SpriteRenderer sr;
    Rigidbody2D rb;
    [SerializeField] float throwSpeed = 10f;
    [SerializeField] float returnSpeed = 10f;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float poiseDamage = 50f;
    [SerializeField] float damage = 0f;
    [SerializeField] float knockBackDamage = 0f;
    float timeToSpriteSwitch = .1f;
    Vector3 initialPosition;
    Vector3 returnPoint;
    Vector2 normalizedDirection;
    bool isReturning = false;
    bool shot;
    private int currentSpriteIndex = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }
    private void Start()
    {
        initialPosition = transform.position;// cache where the boomerang is originally thrown from

    }
    void Shoot(Projectile component, Vector2 direction)
    {
        if (!shot)
        {
            transform.position = component.transform.position;
            initialPosition = component.transform.position;
            // set shoot to true so it can start finding player transform while active for 
            // the return
            InvokeRepeating("SwitchSprite", 0.0f, timeToSpriteSwitch);
            StartCoroutine(AutoPickUP());
            //apply force and direction

            returnPoint = initialPosition;         //caching for the return
             normalizedDirection = direction.normalized;
            rb.velocity = normalizedDirection * throwSpeed; //normalize to get equal diagonal speeds
            shot = true;
        }
    }

    private void Update()
    {
        if (shot)
        {
            if (isReturning)
            {
                Vector2 returnDirection = (returnPoint - transform.position).normalized;

                rb.velocity = returnDirection * returnSpeed;

                if (Vector3.Distance(transform.position, returnPoint) < 1.0f)
                {
                    // Boomerang has returned, make it still
                    rb.velocity = new Vector2(0, 0);
                    //stop sprite rotation
                    CancelInvoke("SwitchSprite");
                    isReturning = false;
                }
            }
            else
            {
                // Check if the boomerang has reached the maximum distance
                if (Vector3.Distance(transform.position, returnPoint) >= maxDistance)
                {
                    // Change the flag to start returning the boomerang
                    isReturning = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the boomerang has collided with something other than the player
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //apply damages
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
            if (collision.TryGetComponent(out IKnockBackable knockBackable))
            {
                knockBackable.KnockBack(normalizedDirection, knockBackDamage, (int)normalizedDirection.x, (int)normalizedDirection.y);
            }
            if (collision.TryGetComponent(out IPoiseDamageable poise))
            {
                poise.DamagePoise(poiseDamage);
            }
            // Boomerang has hit an enemy, return it immediately
            isReturning = true;
        }
    }

    public void Interact() //player has to pick up boomerang to throw again. 
    {
        shot = false;
        gameObject.SetActive(false);
    }
    void SwitchSprite()
    {
        // Switch to the next sprite in the list
        currentSpriteIndex = (currentSpriteIndex + 1) % boomerangSprites.Count;
        sr.sprite = boomerangSprites[currentSpriteIndex];
    }
    IEnumerator AutoPickUP()
    {
        yield return new WaitForSeconds(15f);
        Interact();
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


