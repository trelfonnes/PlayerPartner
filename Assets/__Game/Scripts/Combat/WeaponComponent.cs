using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponComponent : MonoBehaviour
{
    protected Weapon weapon;
    protected PartnerWeapon partnerWeapon;

    protected bool isAttackActive;
    protected bool isPartnerAttackActive;

    protected virtual void Awake()
    {
        weapon = GetComponent<Weapon>();
        partnerWeapon = GetComponent<PartnerWeapon>();
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
