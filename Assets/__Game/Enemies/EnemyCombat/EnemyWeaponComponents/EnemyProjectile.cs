using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : WeaponComponent<ProjectileData, AttackProjectileData>
{
    EnemyMovement movement;
    Vector2 direction; //comes from lastenemydirection in enemymovement ref

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
        direction = movement.LastEnemyDirection; //new Vector2(movement.LastEnemyDirection.x, movement.LastEnemyDirection.y);
      ProjectileEventSystem.Instance.RaiseEnemyDirectionSetEvent(this, direction, currentAttackDataEnemy.damage, currentAttackDataEnemy.knockbackStrength);
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
