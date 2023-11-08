using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : Movement
{
    //Add enemy specific functions and pass to set final velocity in base movement.
    float directionX;
    float directionY;
    Transform partnerTransform;
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
        workspace.Set(directionX, directionY);
        SetFinalVelocity();
    }

    public void ChargePartner(float velocity)
    {
        partnerTransform = GameObject.FindGameObjectWithTag("Partner").transform;
        workspace = Vector2.MoveTowards(this.transform.position, partnerTransform.position, velocity * Time.deltaTime);
        SetFinalVelocity();
    }
    public void Flee(float velocity)
    {
        int randomValueX = Random.Range(-1, 2);
        directionX = randomValueX * velocity;
        int RandomValueY = Random.Range(-1, 2);
        directionY = RandomValueY * velocity;
        workspace.Set(directionX, directionY);
        SetFinalVelocity();
    }

}
