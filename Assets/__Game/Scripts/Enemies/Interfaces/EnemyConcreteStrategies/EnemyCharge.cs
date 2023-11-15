using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharge : IEnemyMove
{
    public void StartMovement(float velocity, EnemyMovement movement, EnemyCollisionSenses collisionSenses)
    {

        if (collisionSenses.partnerTransform)
        {
            movement.ChargePartner(velocity, collisionSenses.partnerTransform);
        }
        else if (collisionSenses.playerTransform)
        {
            movement.ChargePartner(velocity, collisionSenses.playerTransform);

        }
        else
            return;
    } 
}
