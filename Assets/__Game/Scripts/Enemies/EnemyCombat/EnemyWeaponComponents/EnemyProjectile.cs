using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : WeaponComponent<ProjectileData, AttackProjectileData>
{
    EnemyMovement movement;
    Vector2 direction; //comes from combatfacingdirection in movement ref

    public void ShootProjectile() //call this from anim event handler
    {
        UnPoolProjectile();
    }
    void UnPoolProjectile()
    {
        ProjectileEventSystem.Instance.RaiseSetProjectileTypeEvent(currentAttackDataEnemy.TypeOfProjectile);
        SetDirection();
    }


    void SetDirection() // listened to by actual projectile game object that is "unpooled"
    {//make sure combat facing direction works for enemy. 
        direction = new Vector2(movement.facingCombatDirectionX, movement.facingCombatDirectionY);
      ProjectileEventSystem.Instance.RaiseEnemyDirectionSetEvent(this, direction);
    }

    protected override void Start()
    {
        base.Start();
        movement = EnemyCore.GetCoreComponent<EnemyMovement>();
        EnemyEventHandler.OnShootProjectile += ShootProjectile;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EnemyEventHandler.OnShootProjectile -= ShootProjectile;
    }
}
