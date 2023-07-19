using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData : ComponentData<AttackDamage>
{
    protected override void SetComponentDependency()
    {
        PlayerComponentDependency = typeof(Damage);
        PartnerComponentDependency = typeof(PartnerDamage);
    }
}
