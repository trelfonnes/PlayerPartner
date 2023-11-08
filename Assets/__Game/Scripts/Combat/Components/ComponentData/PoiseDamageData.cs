using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseDamageData : ComponentData<AttackPoiseDamage>
{
    protected override void SetComponentDependency()
    {
        PlayerComponentDependency = typeof(PoiseDamage);
        PartnerComponentDependency = typeof(PartnerPoiseDamage);
        EnemyComponentDependency = typeof(EnemyPoiseDamage);
    }
}
