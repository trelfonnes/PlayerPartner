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
  

    public void EnemyCheckIfShouldFlip( Vector2 vector2)
    {
        if(vector2.x < 0)
        {
            rb.transform.localScale = new Vector3(-1, 1, 1);
        } 
        else if(vector2.x > 0)
        {
            rb.transform.localScale = new Vector3(1, 1, 1);
        }
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
            Vector2 targetPosition = Vector2.MoveTowards(this.transform.position, CharacterTransform.position, velocity * Time.deltaTime);
            Vector2 direction = (targetPosition - (Vector2)this.transform.position).normalized;
            enemy.anim.SetFloat("moveX", direction.x);
            enemy.anim.SetFloat("moveY", direction.y);
            enemy.enemyDirection = direction;
            rb.transform.position = targetPosition;
            UpdateLastDirection(direction.x, direction.y);
            EnemyCheckIfShouldFlip(direction);

        }
        else 
            Debug.LogWarning("CharacterTransform is null in ChargePartner method");
        return;
    }
    public void Chase(float velocity, Transform characterTransform)
    {

    }
    public void Patrol()
    {
        Vector2 patrolDirection = new Vector2(LastEnemyDirection.x, LastEnemyDirection.y).normalized;
        workspace.Set(patrolDirection.x, patrolDirection.y);
        SetFinalVelocity();
    }
    public void Flee(float velocity)
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
    void UpdateLastDirection(float X, float Y)
    {
        directionX = X;
        directionY = Y;
        LastEnemyDirection = new Vector2(X, Y);
    }

}
