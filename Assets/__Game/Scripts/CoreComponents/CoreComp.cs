using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComp<T> where T : CoreComponent
{
    CoreHandler core;
    T comp;

    public T Comp => comp ? comp : core.GetCoreComponent(ref comp);


    public CoreComp(CoreHandler core)
    {
        if(core == null)
        {
            Debug.LogWarning($"From Trel: Core is null for component{typeof(T)}");
        }

        this.core = core;
    }
}
