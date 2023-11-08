using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMovement : WeaponComponent<MovementData, AttackMovementData>
{
    EnemyMovement movement;

    void HandleStartMovement()
    {
        // TODO create facing directions movement.SetVelocity(currentAttackDataEnemy.Direction * )
    }

    void HandleStopMovement()
    {
        movement.SetVelocityZero();
    }

    protected override void Start()
    {
        base.Start();
        movement = EnemyCore.GetCoreComponent<EnemyMovement>();
        EnemyEventHandler.OnStartMovement += HandleStartMovement;
        EnemyEventHandler.OnStopMovement += HandleStopMovement;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EnemyEventHandler.OnStartMovement -= HandleStartMovement;
        EnemyEventHandler.OnStopMovement -= HandleStopMovement;
    }

}
