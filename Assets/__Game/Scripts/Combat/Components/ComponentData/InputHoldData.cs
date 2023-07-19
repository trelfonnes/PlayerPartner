using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHoldData : ComponentData //base version because no attack data
{
    protected override void SetComponentDependency()
    {
        PlayerComponentDependency = typeof(InputHold);
        PartnerComponentDependency = typeof(PartnerInputHold);
    }
}
