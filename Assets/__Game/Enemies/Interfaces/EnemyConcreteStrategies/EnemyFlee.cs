using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlee : IEnemyLowHealth
{
    public void StartLowHealthStrategy(EnemySOData data, EnemyMovement movement, EnemyCollisionSenses collisionSenses)
    {
        movement.Flee(data.lowHealthSpeed);

    }
}
