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
            enemy.enemyDirection = LastEnemyDirection;
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
    }
}

