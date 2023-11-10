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
  

    public void EnemyCheckIfShouldFlip(int directionX)
    {
        if (directionX < 0 && directionX != facingCombatDirectionX)
        {
            FlipX();
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
    }

    public void ChargePartner(float velocity, Transform CharacterTransform)
    {
        if (CharacterTransform != null)
        {
            workspace = Vector2.MoveTowards(this.transform.position, CharacterTransform.position, velocity * Time.deltaTime);
            SetFinalVelocity();
            UpdateLastDirection(CurrentVelocity.x, CurrentVelocity.y);
        }
        else
            return;
    }
    public void Chase(float velocity, Transform characterTransform)
    {

    }
    public void Patrol()
    {
        workspace.Set(LastEnemyDirection.x, LastEnemyDirection.y);
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
    }
    void UpdateLastDirection(float X, float Y)
    {
        LastEnemyDirection = new Vector2(X, Y);
    }

}
