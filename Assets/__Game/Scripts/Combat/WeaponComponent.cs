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
    }
    protected virtual void HandleExit()
    {
        isAttackActive = false;
    }
    protected virtual void HandlePartnerEnter()
    {
        isPartnerAttackActive = true;
    }
    protected virtual void HandlePartnerExit()
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
    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2: AttackData
    {
        protected T1 dataPartner;
        protected T1 dataPlayer;
    protected T2 currentAttackDataPartner;
    protected T2 currentAttackDataPlayer;


    protected override void HandleEnter()
    {
        base.HandleEnter();
        
        if (weapon != null)
        {
            currentAttackDataPlayer = dataPlayer.AttackData[weapon.CurrentAttackCounter];
        }
    }
    protected override void HandlePartnerEnter()
    {
        base.HandlePartnerEnter();
        if (partnerWeapon != null)
        {
            currentAttackDataPartner = dataPartner.AttackData[partnerWeapon.CurrentAttackCounter];
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (partnerWeapon != null)
        {
            dataPartner = partnerWeapon.Data.GetData<T1>();
        }
        if(weapon != null)
        {
            dataPlayer = weapon.Data.GetData<T1>();

        }
    }

    }


