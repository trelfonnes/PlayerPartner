using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : BossCoreComponent
{
    Rigidbody2D rb;
    //Variables for ArenaBattles
    Vector2 randomDistancingDirection = Vector2.zero;
    bool isKnockedback = false;
    float knockbackEndTime;
    Vector2 defaultMovingDirection = Vector2.down;
    private bool canMove = true;
    [SerializeField] EnemyStatEvents bossStatEvents;

    public Vector2 CurrentDirection { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponentInParent<Rigidbody2D>();
        bossStatEvents.onStaminaZero += StopMovement;
        
    }
    private void OnDisable()
    {
        bossStatEvents.onStaminaZero -= StopMovement;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
    public void MoveTowards(Vector2 targetPosition, float moveSpeed)
    {
        Vector2 direction = (targetPosition - rb.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

   public void HoldMovementForMelee()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void ContinueMovement(float timeToMove)
    {
        StartCoroutine(MoveAgain(timeToMove));
    }
    IEnumerator MoveAgain(float timeToMove)
    {
        yield return new WaitForSeconds(timeToMove);
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }
    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }
    public void Teleport(float minTeleportDistance, float maxTeleportDistance)
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        // Calculate a random distance between min and max teleport distances
        float randomDistance = Random.Range(minTeleportDistance, maxTeleportDistance);

        // Calculate the new position based on the random angle and distance
        Vector3 newPosition = rb.transform.position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * randomDistance;

        // Teleport the enemy to the new position instantly
        rb.transform.position = newPosition;
    }


    //Methods for Arena Battles
    public void ChargePlayer(float moveSpeed, Transform player, float buffer)
    {
        if (!isKnockedback)
        {
            Vector2 directionToPlayer = player.position - rb.transform.position;

            // Calculate the distance between the enemy and the player
            float distanceToPlayer = directionToPlayer.magnitude;

            // Check if the enemy is within the buffer distance from the player
            if (distanceToPlayer > buffer)
            {
                // Normalize the direction vector
                Vector2 moveDirection = directionToPlayer.normalized;

                // Calculate the velocity with the desired speed
                Vector2 velocity = moveDirection * moveSpeed;

                // Set the rigidbody velocity to move the enemy towards the player
                CurrentDirection = velocity.normalized;
                CheckIfShouldFlip(velocity);
                rb.velocity = velocity;
            }
        }
    }
    float timeSinceLastDirectionChange = 0f;
    public void KeepDistance(float moveSpeed, Transform player, float distance, bool isTouchingWall)
    {
      
        float directionChangeCooldown = 1.5f; // Adjust as needed

        if (!isKnockedback)
        {
            Vector2 directionToPlayer = player.position - rb.transform.position;
            float currentDistance = directionToPlayer.magnitude;
            if (currentDistance < distance) 
            {
                if (timeSinceLastDirectionChange > directionChangeCooldown|| randomDistancingDirection == Vector2.zero)
                {
                    if (isTouchingWall )
                    {
                        randomDistancingDirection = -randomDistancingDirection;
                        timeSinceLastDirectionChange = 0f;
                    }
                    else
                    {
                        randomDistancingDirection = GenerateRandomDirection();
                        timeSinceLastDirectionChange = 0f; // Reset cooldown
                    }                            //  }
                }
            }

            Vector2 movement = randomDistancingDirection * moveSpeed * Time.deltaTime;
            FaceThePlayer(player);
            //CheckIfShouldFlip(CurrentDirection);
            rb.position -= movement;

            // Increment time since last direction change
            timeSinceLastDirectionChange += Time.deltaTime;
        }
    }
    public void FaceThePlayer(Transform playerTransform)
    {
        
        // Calculate the relative position and direction
        Vector3 relativeDirection = (playerTransform.position - transform.position).normalized;

        // Use this relative direction to determine which way the enemy should face
        CheckIfShouldFlip(relativeDirection);
       
        
    }
    public void CheckIfShouldFlip(Vector2 vector2)
    {
        if (vector2.x < 0)
        {
            rb.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (vector2.x > 0)
        {
            rb.transform.localScale = new Vector3(1, 1, 1);
        }

    }


    Vector2 GenerateRandomDirection()
    {
        int[] validDirections = { -1, 1 };
        int randomX = validDirections[Random.Range(0, validDirections.Length)];
        int randomY = validDirections[Random.Range(0, validDirections.Length)];

        // Prevent zero-zero direction
        if (randomX == 0 && randomY == 0)
        {
            randomX = Random.Range(-1, 2); // Random value between -1 and 1 (inclusive)
            randomY = Random.Range(-1, 2); // Random value between -1 and 1 (inclusive)
        }

        return new Vector2(randomX, randomY).normalized;
    }
    float GenerateRandomDistance(float distance)
    {
        return Random.Range(2, distance);
    }

    //Knockback related

    public void SetKnockBackVelocity(Vector2 angle, float strength, int directionX, int directionY)
    {
        rb.velocity = Vector2.zero; //stop movement for application of knockback
        if (!isKnockedback && this.isActiveAndEnabled)
        {
            isKnockedback = true;
            Vector2 knockbackDir = new Vector2(directionX * strength, directionY * strength);
            rb.AddForce(knockbackDir, ForceMode2D.Impulse);//fiddle here for desired effect
            CurrentDirection = -knockbackDir.normalized;
            knockbackEndTime = Time.time + 0.2f;
            StartCoroutine(EndKnockback());
        }
    }
    private IEnumerator EndKnockback()
    {
        while (Time.time < knockbackEndTime)
        {
           
            yield return null;
        }
        rb.velocity = Vector2.zero;
        isKnockedback = false;

    }

    public void MoveOnOff(bool onOff)
    {
        canMove = onOff;
        if (!canMove && !isKnockedback)
        {
            rb.velocity = Vector2.zero;
        }
    }
    public bool CanMove()
    {
        return canMove;
    }
}
