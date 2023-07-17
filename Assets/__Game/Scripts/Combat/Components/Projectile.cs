using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : WeaponComponent<ProjectileData, AttackProjectileData>
{
    Movement movement;
    Vector2 direction;

    public void ShootProjectile()
    {
        UnPoolProjectile();
    }
    void UnPoolProjectile()
    {
        ProjectileEventSystem.Instance.RaiseSetProjectileTypeEvent(currentAttackDataPlayer.TypeOfProjectile);
        SetDirection();
    }
    void SetDirection()
    {
       
        direction = new Vector2(movement.facingCombatDirectionX, movement.facingCombatDirectionY);
        ProjectileEventSystem.Instance.RaisePlayerDirectionSetEvent(this, direction);
    }

    protected override void Start()
    {
        base.Start();
        movement = PlayerCore.GetCoreComponent<Movement>();
        PlayerEventHandler.OnShootProjectile += ShootProjectile;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        PlayerEventHandler.OnShootProjectile -= ShootProjectile;
    }


}
