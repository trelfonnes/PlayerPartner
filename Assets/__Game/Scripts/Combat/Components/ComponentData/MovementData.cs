using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementData : ComponentData<AttackMovementData>
{
    public MovementData()
    {
        PlayerComponentDependency = typeof(AttackMovement);
        PartnerComponentDependency = typeof(PartnerAttackMovement);
    }
    
}
