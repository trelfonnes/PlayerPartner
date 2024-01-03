using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteData : ComponentData<AttackIgnite>
{
    protected override void SetComponentDependency()
    {
        PartnerComponentDependency = typeof(PartnerIgnite);
    }
}
