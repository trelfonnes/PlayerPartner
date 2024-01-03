using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrodeData : ComponentData<AttackCorrode>
{
    protected override void SetComponentDependency()
    {
        PartnerComponentDependency = typeof(PartnerCorrode);

    }
}
