using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharge : IEnemyMove
{
    public void StartMovement(float velocity, EnemyMovement movement)
    {
        movement.ChargePartner(velocity);
    } 
}
