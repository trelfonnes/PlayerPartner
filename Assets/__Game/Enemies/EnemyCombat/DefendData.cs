using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendData : ComponentData<AttackDefend>
{
    protected override void SetComponentDependency()
    {
        EnemyComponentDependency = typeof(EnemyDefendComponent);
        PlayerComponentDependency = typeof(Defend);

    }
}
