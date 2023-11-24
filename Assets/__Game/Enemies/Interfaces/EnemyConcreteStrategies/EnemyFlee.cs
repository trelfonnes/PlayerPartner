using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlee : IEnemyLowHealth
{
    public void StartLowHealthStrategy(float velocity, EnemyMovement movement)
    {
        movement.Flee(velocity);

    }
}
