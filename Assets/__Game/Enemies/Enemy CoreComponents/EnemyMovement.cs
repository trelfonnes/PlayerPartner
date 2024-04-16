using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : Movement
{
    //Add enemy specific functions and pass to set final velocity in base movement.
    float directionX;
    float directionY;
    Transform partnerTransform;
    public Vector2 LastEnemyDirection { get; private set; }

    protected override void Awake()
    {
        base.Awake();

    }
    // Combat changing is a function in movement that receives to int values for direction x and y


    public void EnemyCheckIfShouldFlip(Vector2 vector2)
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
    public void TurnTowardsAttack(int directionX, int directionY)
    {
        float angle = Mathf.Atan2(-directionY, -directionX) * Mathf.Rad2Deg;

        // Negate the angle to make the enemy turn towards the correct direction
        transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        UpdateLastDirection(-directionX, -directionY); // for the lastenemydirection
        EnemyCheckIfShouldFlip(LastEnemyDirection); // for the x axis
        enemy.enemyDirection = LastEnemyDirection;// for the FOV
        enemy.anim.SetFloat("moveX", LastEnemyDirection.x); //ForThe Anim
        enemy.anim.SetFloat("moveY", LastEnemyDirection.y);
    }
    public void ChangeDirection(float velocity)
    {
        int randomValueX = Random.Range(-1, 2);
        directionX = randomValueX * velocity;
        int RandomValueY = Random.Range(-1, 2);
        directionY = RandomValueY * velocity;

        UpdateLastDirection(directionX, directionY);
        workspace.Set(directionX, directionY);
        SetFinalVelocity();
        EnemyCheckIfShouldFlip(CurrentVelocity);

    }
    public void Pursue( Transform target)
    {
       
        Vector2 direction = (target.position - rb.transform.position).normalized;

        rb.AddForce(direction * 4f, ForceMode2D.Impulse);

    }
    public void ChargePartner(float velocity, Transform CharacterTransform)
    {
        if (CharacterTransform != null)
        {

            if (canReceiveInput)
            {

                Vector2 targetPosition = Vector2.MoveTowards(this.transform.position, CharacterTransform.position, velocity * Time.deltaTime);
                Vector2 direction = (targetPosition - (Vector2)this.transform.position).normalized;
                Vector2 roundedDirection = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));

                enemy.anim.SetFloat("moveX", roundedDirection.x);
                enemy.anim.SetFloat("moveY", roundedDirection.y);
                enemy.enemyDirection = roundedDirection;

                rb.transform.position = targetPosition;
                UpdateLastDirection(roundedDirection.x, roundedDirection.y);
                EnemyCheckIfShouldFlip(direction);
            }

        }
        else
            Debug.LogWarning("CharacterTransform is null in ChargePartner method");
        return;
    }

    public void Patrol()
    {
        if (canReceiveInput)
        {
            Vector2 patrolDirection = new Vector2(LastEnemyDirection.x, LastEnemyDirection.y).normalized;
            workspace.Set(patrolDirection.x, patrolDirection.y);
            enemy.anim.SetFloat("moveX", patrolDirection.x);
            enemy.anim.SetFloat("moveY", patrolDirection.y);
            enemy.enemyDirection = patrolDirection;
            SetFinalVelocity();
        }
    }
    public void Flee(float velocity)
    {
        if (canReceiveInput)
        {
            int randomValueX = Random.Range(-1, 2);
            directionX = randomValueX * velocity;
            int RandomValueY = Random.Range(-1, 2);
            directionY = RandomValueY * velocity;
            UpdateLastDirection(directionX, directionY);
            workspace.Set(LastEnemyDirection.x, LastEnemyDirection.y);
            enemy.anim.SetFloat("moveX", LastEnemyDirection.x);
            enemy.anim.SetFloat("moveY", LastEnemyDirection.y);
            SetFinalVelocity();
            EnemyCheckIfShouldFlip(LastEnemyDirection);

        }
    }
   
    void UpdateLastDirection(float X, float Y)
    {
        directionX = X;
        directionY = Y;
        LastEnemyDirection = new Vector2(X, Y);
        if (enemy)
        {
            enemy.enemyDirection = LastEnemyDirection;
        }
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
    private bool isCircling = false; // Flag to track if the enemy is circling
    private float circleRadius = 10f; // Radius for circling around the character
    public float approachRadius = 2f;
    public void MoveInCircularMotion(Transform characterTransform, float radius, float speed)
    {
        // Calculate the distance to the character
        
            float distanceToCharacter = Vector3.Distance(rb.transform.position, characterTransform.position);

            // Check if the enemy is within the approach radius
            if (distanceToCharacter <= approachRadius)
            {
                isCircling = true;
            }
            else
            {
            // Player moved out of circling range, reset flag
                isCircling = false;
                rb.velocity = Vector2.zero;
            }

            if (isCircling)
            {
            CircleCharacter(speed);
            }
            else
            {

            Vector3 directionToCharacter = characterTransform.position - rb.transform.position;

            // Calculate the angle from the direction
            float angle = Mathf.Atan2(directionToCharacter.y, directionToCharacter.x);

            // Update the angle based on time and speed
            angle += Time.deltaTime * speed;

            // Calculate the new position based on circular motion
            float xPosition = characterTransform.position.x + Mathf.Cos(angle) * radius;
            float yPosition = characterTransform.position.y + Mathf.Sin(angle) * radius;

            // Move towards the character transform
            Vector3 newPosition = Vector3.MoveTowards(rb.transform.position, new Vector3(xPosition, yPosition, rb.transform.position.z), speed * Time.deltaTime);

            // Update the enemy's position
            rb.transform.position = newPosition;
        
    }
        }
    private void CircleCharacter(float speed)
    {
        float angle = Time.time * (speed);

        // Calculate the new position based on circular motion
        float xPosition = Mathf.Cos(angle) * (speed);
        float yPosition = Mathf.Sin(angle) * (speed);
        

        // Move towards the calculated position
        rb.velocity = new Vector2(xPosition, yPosition);
    }
}



