using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteData : ComponentData<AttackSprites>
{
    WeaponSpriteData()
    {
        PlayerComponentDependency = typeof(WeaponSprite);
        PartnerComponentDependency = typeof(PartnerWeaponSprite);
    }

}
