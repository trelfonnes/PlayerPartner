using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreComp<T> where T : BossCoreComponent
{
    BossComponentLocator compLoc;
    T comp;
    public T Comp => comp ? comp : compLoc.GetCoreComponent(ref comp);

    public BossCoreComp(BossComponentLocator compLoc)
    {
        if(compLoc == null)
        {
            Debug.LogWarning($"From Trel: BosscoreComp is null for component{typeof(T)}");

        }
        this.compLoc = compLoc;
    }

}
