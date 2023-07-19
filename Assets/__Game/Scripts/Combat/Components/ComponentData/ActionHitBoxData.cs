using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHitBoxData : ComponentData<AttackActionHItBox>
{
 [field: SerializeField] public LayerMask DetectableLayers { get; private set; }

  protected override void SetComponentDependency()
    {
        PlayerComponentDependency = typeof(ActionHitBox);
        PartnerComponentDependency = typeof(PartnerActionHitBox);
    }
}
