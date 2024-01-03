using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlee : IEnemyLowHealth
{
    public void StartLowHealthStrategy(EnemySOData data, EnemyMovement movement)
    {
        movement.Flee(data.lowHealthSpeed);

    }
}
