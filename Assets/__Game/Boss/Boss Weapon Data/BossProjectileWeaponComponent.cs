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


    void SetDirection() // listened to by actual projectile game object that is "unpooled"
    {//make sure combat facing direction works for enemy. 
       // direction = movement.LastEnemyDirection; //new Vector2(movement.LastEnemyDirection.x, movement.LastEnemyDirection.y);
        ProjectileEventSystem.Instance.RaiseBossDirectionSetEvent(this.transform.position, Vector2.zero);
    }

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
