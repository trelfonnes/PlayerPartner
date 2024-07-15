using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : WeaponComponent<TeleportData, AttackTeleport>
{
    BossMovement movement;


    protected override void Start()
    {
        base.Start();
        BossEventHandler.onChangePosition += StartTeleportation;
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        BossEventHandler.onChangePosition -= StartTeleportation;

    }

    void StartTeleportation()
    {
        movement.Teleport(currentAttackDataBoss.minTeleportDistance, currentAttackDataBoss.maxTeleportDistance);
    }
}
