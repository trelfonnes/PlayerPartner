using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleportWeapon : WeaponComponent<TeleportData, AttackTeleport>
{
    EnemyMovement movement;

    protected override void Start()
    {
        base.Start();
        movement = EnemyCore.GetCoreComponent<EnemyMovement>();
        EnemyEventHandler.onChangePosition += StartTeleportation;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        EnemyEventHandler.onChangePosition -= StartTeleportation;

    }
    void StartTeleportation()
    {
        movement.Teleport(currentAttackDataEnemy.minTeleportDistance, currentAttackDataEnemy.maxTeleportDistance);

    }
    

}
