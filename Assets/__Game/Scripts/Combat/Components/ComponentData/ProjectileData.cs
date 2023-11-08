using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : ComponentData<AttackProjectileData>
{
    protected override void SetComponentDependency()
    {
        PlayerComponentDependency = typeof(Projectile);
        PartnerComponentDependency = typeof(PartnerProjectile);
        EnemyComponentDependency = typeof(EnemyProjectile);
    }
}
