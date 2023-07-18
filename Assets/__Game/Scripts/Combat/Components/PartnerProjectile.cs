using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerProjectile : WeaponComponent<ProjectileData, AttackProjectileData>
{
    Movement movement;
    Vector2 direction;// comes from combatfacingdirection in movment ref above 
    SpecialPower specialPower;

    public void ShootProjectile() // call this from anim event handler
    {
        UnPoolProjectile();// TODO: logic for checking SP and for decreasing SP
    }
    void UnPoolProjectile() //listed to (through Event Handler script) by pooling script
    {
        ProjectileEventSystem.Instance.RaiseSetProjectileTypeEvent(currentAttackDataPartner.TypeOfProjectile);
        SetDirection();
    }
    void SetDirection() // listened to by actual projectile game object that is "unpooled"
    {
        direction = new Vector2(movement.facingCombatDirectionX, movement.facingCombatDirectionY);
        ProjectileEventSystem.Instance.RaisePartnerDirectionSetEvent(this, direction);
    }
    protected override void Start()
    {
        base.Start();
        movement = PartnerCore.GetCoreComponent<Movement>();
        specialPower = PartnerCore.GetComponent<SpecialPower>();
        PartnerEventHandler.OnShootProjectile += ShootProjectile;//can create event to have a dropdown like Phases that designates which projectile to shoot??
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        PartnerEventHandler.OnShootProjectile -= ShootProjectile;
    }
}
