using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WeaponComponent : MonoBehaviour
{
    protected Weapon weapon;
    protected PartnerWeapon partnerWeapon;
    protected Partner partner;
    protected Player player;
    protected bool isAttackActive;
    protected bool isPartnerAttackActive;

    // protected AnimationEventHandler PlayerEventHandler => weapon.EventHandler;
    // protected AnimationEventHandler PartnerEventHandler => partnerWeapon.EventHandler;

    //TODO: switch these below to top and remove from awake Once ready.

    protected AnimationEventHandler PartnerEventHandler;
    protected AnimationEventHandler PlayerEventHandler;
    protected CoreHandler PlayerCore => weapon?.Core;
    protected CoreHandler PartnerCore => partnerWeapon?.Core;

    protected virtual void Awake()
    {
            
            partnerWeapon = GetComponent<PartnerWeapon>();
            partner = GetComponentInParent<Partner>();
            PartnerEventHandler = GetComponentInChildren<AnimationEventHandler>();
        weapon = GetComponent<Weapon>();
        player = GetComponentInParent<Player>();
        PlayerEventHandler = GetComponentInChildren<AnimationEventHandler>();

    }

    protected virtual void HandleEnter()
    {
        isAttackActive = true;
    }protected virtual void HandleExit()
    {
        isAttackActive = false;
    }
    protected virtual void HandlePartnerEnter()
    {
        isPartnerAttackActive = true;
    }protected virtual void HandlePartnerExit()
    {
        isPartnerAttackActive = false;
    }
    protected virtual void OnEnable()
    {
        if (weapon != null)
        {
            weapon.onEnter += HandleEnter;
            weapon.onExit += HandleExit;
        }
        if (partnerWeapon != null)
        {
            partnerWeapon.onEnter += HandlePartnerEnter;
            partnerWeapon.onExit += HandlePartnerExit;
        }
    }
    protected virtual void OnDisable()
    {
        if (weapon != null)
        {
            weapon.onEnter -= HandleEnter;
            weapon.onExit -= HandleExit;
        }
        if (partnerWeapon != null)
        {
            partnerWeapon.onEnter -= HandlePartnerEnter;
            partnerWeapon.onExit -= HandlePartnerExit;
        }
    }

}
