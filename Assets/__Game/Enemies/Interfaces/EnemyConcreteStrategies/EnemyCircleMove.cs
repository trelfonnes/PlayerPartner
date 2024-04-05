using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleMove : IEnemyMove
{
   public void StartMovement(float velocity, EnemyMovement movement, EnemyCollisionSenses collisionSenses)
    {
        if (collisionSenses.partnerTransform)
        {
            movement.MoveInCircularMotion(collisionSenses.partnerTransform, 5f, velocity);
        }
        else if (collisionSenses.playerTransform)
        {
            movement.MoveInCircularMotion(collisionSenses.playerTransform, 5f, velocity);
        }
        else
            return;
    }
}
