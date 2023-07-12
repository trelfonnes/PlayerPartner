using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackData : ComponentData<AttackKnockBack> //component data is now an abstract class which requries us to inherit the abstract function that is below
{
    protected override void SetComponentDependency()
    {
        PlayerComponentDependency = typeof(KnockBack);
        PartnerComponentDependency = typeof(PartnerKnockBack);
    }
}
