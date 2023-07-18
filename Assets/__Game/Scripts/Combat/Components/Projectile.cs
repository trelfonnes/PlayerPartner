using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : WeaponComponent<ProjectileData, AttackProjectileData>
{
    Movement movement;
    SpecialPower specialPower;
    Vector2 direction;

    public void ShootProjectile()
    {
        UnPoolProjectile(); //TODO: logic for checking SP and for decreasing SP
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
        specialPower = PlayerCore.GetCoreComponent<SpecialPower>();
        PlayerEventHandler.OnShootProjectile += ShootProjectile;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        PlayerEventHandler.OnShootProjectile -= ShootProjectile;
    }


}
