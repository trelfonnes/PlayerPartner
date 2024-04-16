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
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponentInParent<Rigidbody2D>();
        
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
                rb.velocity = velocity;
            }
        }
    }
    public void KeepDistance(float moveSpeed, Transform player, float distance, bool isTouchingWall)
    {
        if (!isKnockedback)
        {
            Vector2 directionToPlayer = player.position - rb.transform.position;
            float currentDistance = directionToPlayer.magnitude;

            // Calculate the target distance from the player
            float targetDistance = distance;

            // If the current distance is less than the target distance, move away from the player
            if (currentDistance < targetDistance)
            {
                if (randomDistancingDirection == Vector2.zero || isTouchingWall) //Collisions hitting wall.
                {
                    // Generate random x and y components for a new movement direction. Should only happen when running into a wall, or vector 2 is zero.
                    // Create a vector with the random x and y components
                    randomDistancingDirection = GenerateRandomDirection();
                }
                // Calculate the movement vector with the desired speed
                Vector2 movement = randomDistancingDirection * moveSpeed * Time.deltaTime;

                // Move the enemy in the opposite direction to maintain distance
                rb.position -= movement;
            }
            else
            {
                randomDistancingDirection = GenerateRandomDirection();
                targetDistance = GenerateRandomDistance(distance);
            }
        }
    }

    Vector2 GenerateRandomDirection()
    {
        int randomX = Random.Range(-1, 2); // Random value between -1 and 1 (inclusive)
        int randomY = Random.Range(-1, 2); // Random value between -1 and 1 (inclusive)
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
}
