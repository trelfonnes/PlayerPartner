using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData : ComponentData<AttackDamage>
{
public DamageData()
    {
        PlayerComponentDependency = typeof(Damage);
        PartnerComponentDependency = typeof(PartnerDamage);
    }

}
