using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadProjectile : MonoBehaviour
{
    ProjectileType spreadProjectile = ProjectileType.SpreadFireProjectile;

    Rigidbody2D rb;
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
    [SerializeField] float separationSpeed = 2f;
    bool hasBeenShot;
    SpreadProjectileLeft leftProj;
    SpreadProjectileRight rightProj;
    private ISpecialAbility specialAbility;
   [SerializeField] bool isActive;
   
    [SerializeField] float maxSeparation = 5f; // Adjust this value based on your preference

    public Vector2 normalizedDirection;
    private bool partnerProjectile;
    private bool enemyProjectile;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        leftProj = GetComponentInChildren<SpreadProjectileLeft>();
        rightProj = GetComponentInChildren<SpreadProjectileRight>();
   
       


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
        
            if (partnerProjectile && collision.CompareTag("Enemy"))
            {
                TakeCareOfCollision(collision);
            }

        if (enemyProjectile && collision.CompareTag("Partner") || enemyProjectile && collision.CompareTag("Player"))
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
        else return;
    }
    private void OnEnable()
    {
        ProjectileEventSystem.Instance.OnPartnerDirectionSet += Shoot;
        ProjectileEventSystem.Instance.OnEnemyDirectionSet += EnemyShoot;
    }


    private void OnDisable()
    {
        ProjectileEventSystem.Instance.OnPartnerDirectionSet -= Shoot;
        ProjectileEventSystem.Instance.OnEnemyDirectionSet -= EnemyShoot;

        isActive = false;
    }


    void Shoot(PartnerProjectile component, Vector2 direction, float damage, float knockBack)
    {
        if (!hasBeenShot)
        {
            this.damage = damage;
            knockBackDamage = knockBack;
            AudioManager.Instance.PlayAudioClip("ShootProjectile");
            partnerProjectile = true;
            enemyProjectile = false;
            float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = rotation;
          
            transform.position = component.transform.position;
            rightProj.transform.position = component.transform.position;
            leftProj.transform.position = component.transform.position;
            
            normalizedDirection = direction.normalized;
            rb.velocity = normalizedDirection * velocity;
            
            rightProj.gameObject.SetActive(true);
            leftProj.gameObject.SetActive(true);
            float offset = 1.50f; // Adjust this value to control the initial separation

            // Calculate the perpendicular vector to the normalized direction
            Vector2 perpendicularVector = new Vector2(-direction.y, direction.x).normalized;

            // Calculate the target positions for left and right projectiles
            Vector3 leftTargetPos = transform.position - (Vector3)perpendicularVector * offset;
            Vector3 rightTargetPos = transform.position + (Vector3)perpendicularVector * offset;

            // Set the initial positions of the child objects
            leftProj.transform.position = transform.position;
            rightProj.transform.position = transform.position;

            // Move the child objects toward their target positions
            StartCoroutine(MoveToTarget(leftProj.transform, leftTargetPos, separationSpeed));
            StartCoroutine(MoveToTarget(rightProj.transform, rightTargetPos, separationSpeed));

            isActive = true;
            normalizedDirection = direction.normalized;

            rb.velocity = normalizedDirection * velocity;

            leftProj.gameObject.SetActive(true);
            rightProj.gameObject.SetActive(true);

            hasBeenShot = true;
            StartCoroutine(SwitchSpriteRoutine());
            StartCoroutine(DeactivateAfterTime());
            rightProj.Shoot(normalizedDirection, enemyProjectile);
            leftProj.Shoot(normalizedDirection, enemyProjectile);


        
        }
    }
    void EnemyShoot(EnemyProjectile component, Vector2 direction, float damage, float knockbackDamage)
    {
               
        if (!hasBeenShot)
        {
            this.damage = damage;
            this.knockBackDamage = knockbackDamage;
            AudioManager.Instance.PlayAudioClip("ShootProjectile");
            partnerProjectile = false;
            enemyProjectile = true;
            float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = rotation;

            transform.position = component.transform.position;
            rightProj.transform.position = component.transform.position;
            leftProj.transform.position = component.transform.position;

            normalizedDirection = direction.normalized;
            rb.velocity = normalizedDirection * velocity;
            Debug.Log("SHOOTING WITH THE ENEMY" + leftProj);
            rightProj.gameObject.SetActive(true);
            leftProj.gameObject.SetActive(true);
            float offset = 1.50f; // Adjust this value to control the initial separation

            // Calculate the perpendicular vector to the normalized direction
            Vector2 perpendicularVector = new Vector2(-direction.y, direction.x).normalized;

            // Calculate the target positions for left and right projectiles
            Vector3 leftTargetPos = transform.position - (Vector3)perpendicularVector * offset;
            Vector3 rightTargetPos = transform.position + (Vector3)perpendicularVector * offset;

            // Set the initial positions of the child objects
            leftProj.transform.position = transform.position;
            rightProj.transform.position = transform.position;

            // Move the child objects toward their target positions
            StartCoroutine(MoveToTarget(leftProj.transform, leftTargetPos, separationSpeed));
            StartCoroutine(MoveToTarget(rightProj.transform, rightTargetPos, separationSpeed));

            isActive = true;
            normalizedDirection = direction.normalized;

            rb.velocity = normalizedDirection * velocity;

            leftProj.gameObject.SetActive(true);
            rightProj.gameObject.SetActive(true);

            hasBeenShot = true;
            StartCoroutine(SwitchSpriteRoutine());
            StartCoroutine(DeactivateAfterTime());
            rightProj.Shoot(normalizedDirection, enemyProjectile);
            leftProj.Shoot(normalizedDirection, enemyProjectile);



        }
    }

    IEnumerator MoveToTarget(Transform projectileTransform, Vector3 targetPosition, float speed)
    {
        float elapsedTime = 0f;
        float moveTime = Vector3.Distance(projectileTransform.position, targetPosition) / speed;

        while (elapsedTime < moveTime)
        {
            projectileTransform.position = Vector3.Lerp(projectileTransform.position, targetPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the projectile reaches exactly the target position
        projectileTransform.position = targetPosition;
    }

   

        }
    

