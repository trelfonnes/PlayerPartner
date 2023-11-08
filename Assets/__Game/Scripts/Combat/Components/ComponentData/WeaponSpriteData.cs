using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteData : ComponentData<AttackSprites>
{
    protected override void SetComponentDependency()
    {
        PlayerComponentDependency = typeof(WeaponSprite);
        PartnerComponentDependency = typeof(PartnerWeaponSprite);
        EnemyComponentDependency = typeof(EnemyWeaponSprite);
    }
}
