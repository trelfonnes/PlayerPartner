using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackMovement : WeaponComponent<MovementData, AttackMovementData>
{
    BossMovement movement;

    void HandleStartMovement()
    {
       // movement.SetVelocity(currentAttackDataEnemy.Direction * movement.LastEnemyDirection);
    }

    void HandleStopMovement()
    {
       // movement.SetVelocityZero();
    }

    protected override void Start()
    {
        base.Start();
        movement = BossCompLoc.GetBossCoreComponent<BossMovement>();
        BossEventHandler.OnStartMovement += HandleStartMovement;
        BossEventHandler.OnStopMovement += HandleStopMovement;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        BossEventHandler.OnStartMovement -= HandleStartMovement;
        BossEventHandler.OnStopMovement -= HandleStopMovement;
    }

}
