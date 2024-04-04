using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportData : ComponentData<AttackTeleport>
{
    protected override void SetComponentDependency()
    {
        EnemyComponentDependency = typeof(EnemyTeleportWeapon);
        //add dependencies for others if they need this. Add data class first.
        //BOSS will need this
        BossComponentDependency = typeof(BossTeleport);
    }
}
