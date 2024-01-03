using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOnData : ComponentData<AttackPowerOn>
{
    protected override void SetComponentDependency()
    {
        PartnerComponentDependency = typeof(PartnerPowerOn);
    }
}
