using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishData : ComponentData<AttackExtinguish>
{
    protected override void SetComponentDependency()
    {
        PartnerComponentDependency = typeof(PartnerExtinguish);

    }
}
