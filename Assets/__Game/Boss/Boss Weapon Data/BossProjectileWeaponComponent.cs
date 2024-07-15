using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileWeaponComponent : WeaponComponent<ProjectileData, AttackProjectileData>
{
    BossMovement movement;
    BossProjectile projectile;
    Vector2 direction; //comes from lastenemydirection in enemymovement ref

    public void ShootProjectile() //call this from anim event handler
    {
        UnPoolProjectile();
    }
    void UnPoolProjectile()
    {
        ProjectileEventSystem.Instance.RaiseSetProjectileTypeEvent(currentAttackDataBoss.TypeOfProjectile);
        SetDirection();
    }


    void SetDirection() 
    {
       // Vector2 direction = CalculateRoundedDirection(projectile.TargetTransform.position);
        ProjectileEventSystem.Instance.RaiseBossDirectionSetEvent(this.transform.position, projectile.ShootDirection, currentAttackDataBoss.damage, currentAttackDataBoss.knockbackStrength);
    }
   // Vector2 CalculateRoundedDirection(Vector3 targetPosition)
   // {
    //    Vector2 normalizedDirection = (new Vector2(targetPosition.x, targetPosition.y)).normalized;
   //     Vector2 roundedDirection = new Vector2(normalizedDirection.x, Mathf.Round(normalizedDirection.y));
    //    return roundedDirection;
   // }
    protected override void Start()
    {
        base.Start();
        movement = BossCompLoc.GetBossCoreComponent<BossMovement>();
        projectile = BossCompLoc.GetBossCoreComponent<BossProjectile>();
        BossEventHandler.OnShootProjectile += ShootProjectile;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        BossEventHandler.OnShootProjectile -= ShootProjectile;
    }
}
