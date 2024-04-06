using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFeint : IEnemyLowHealth
{
    public void StartLowHealthStrategy(EnemySOData data, EnemyMovement movement, EnemyCollisionSenses collisionSenses)
    {
        movement.SetVelocityZero(); // stay in place and play feinting(lowHealth animation)
    }
}
