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
        if (specialPower.canShoot)
        {
            UnPoolProjectile();// TODO: logic for checking SP and for decreasing SP
        }
        else
            Debug.Log("No more SP!!!");
    }

    public void ShootChargedProjectile()
    {
        if (specialPower.canShoot)
        {
            UnPoolChargedProjectile();
        }

    }



    void UnPoolProjectile() //listed to (through Event Handler script) by pooling script
    {
        ProjectileEventSystem.Instance.RaiseSetProjectileTypeEvent(currentAttackDataPartner.TypeOfProjectile);
        SetDirection();
        specialPower.DecreaseSP(currentAttackDataPartner.SPCost);
    }

    void UnPoolChargedProjectile()
    {
        ProjectileEventSystem.Instance.RaiseSetProjectileTypeEvent(currentAttackDataPartner.TypeOfProjectile);
        SetCharge();
        SetDirection();
        specialPower.DecreaseSP(5);// hard code for now, might not need to change
    }

    void SetDirection() // listened to by actual projectile game object that is "unpooled"
    {
        direction = new Vector2(movement.facingCombatDirectionX, movement.facingCombatDirectionY);
        ProjectileEventSystem.Instance.RaisePartnerDirectionSetEvent(this, direction, currentAttackDataPartner.damage, currentAttackDataPartner.knockbackStrength);
    }
    void SetCharge()
    {
        bool charged = true;
        ProjectileEventSystem.Instance.RaisePartnerShotIsCharged(charged);
    }
    protected override void Start()
    {
        base.Start();
        movement = PartnerCore.GetCoreComponent<Movement>();
        specialPower = PartnerCore.GetCoreComponent<SpecialPower>();
        PartnerEventHandler.OnShootProjectile += ShootProjectile;//can create event to have a dropdown like Phases that designates which projectile to shoot??
        PartnerEventHandler.OnShootChargedProjectile += ShootChargedProjectile;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        PartnerEventHandler.OnShootProjectile -= ShootProjectile;
        PartnerEventHandler.OnShootChargedProjectile -= ShootChargedProjectile;
    }
}
