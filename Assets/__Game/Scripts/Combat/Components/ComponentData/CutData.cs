using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutData : ComponentData<AttackCut>
{
    // Start is called before the first frame update
    protected override void SetComponentDependency()
    {
        PlayerComponentDependency = typeof(Cut);

    }
}

