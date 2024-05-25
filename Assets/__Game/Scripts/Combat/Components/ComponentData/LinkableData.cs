using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkableData : ComponentData<AttackLinkable>
{
    protected override void SetComponentDependency()
    {
        PlayerComponentDependency = typeof(Linkable);
    }
}
