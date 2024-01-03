using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementData : ComponentData<AttackMovementData>
{
    protected override void SetComponentDependency()
    {
        PlayerComponentDependency = typeof(AttackMovement);
        PartnerComponentDependency = typeof(PartnerAttackMovement);
        EnemyComponentDependency = typeof(EnemyAttackMovement);
    }
}
