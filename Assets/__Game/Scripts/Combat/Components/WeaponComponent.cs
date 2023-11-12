using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WeaponComponent : MonoBehaviour
{
    protected Weapon weapon;
    protected PartnerWeapon partnerWeapon;
    protected EnemyWeapon enemyWeapon;
    protected Enemy enemy;
    protected Partner partner;
    protected Player player;
    protected bool isAttackActive;
    protected bool isPartnerAttackActive;
    protected bool isEnemyAttackActive;

    // protected AnimationEventHandler PlayerEventHandler => weapon.EventHandler;
    // protected AnimationEventHandler PartnerEventHandler => partnerWeapon.EventHandler;

    //TODO: switch these below to top and remove from awake Once ready.

    protected AnimationEventHandler PartnerEventHandler;
    protected AnimationEventHandler PlayerEventHandler;
    protected AnimationEventHandler EnemyEventHandler;
    protected CoreHandler PlayerCore => weapon?.Core;
    protected CoreHandler PartnerCore => partnerWeapon?.Core;
    protected CoreHandler EnemyCore => enemyWeapon?.Core;

    public virtual void Init()
    {

    }

    protected virtual void Awake()
    {

        partnerWeapon = GetComponent<PartnerWeapon>();
        partner = GetComponentInParent<Partner>();
        
        PartnerEventHandler = GetComponentInChildren<AnimationEventHandler>();
        weapon = GetComponent<Weapon>();
        player = GetComponentInParent<Player>();

        PlayerEventHandler = GetComponentInChildren<AnimationEventHandler>();

        enemy = GetComponentInParent<Enemy>();
        enemyWeapon = GetComponent<EnemyWeapon>();
        EnemyEventHandler = GetComponentInChildren<AnimationEventHandler>();
    }
    protected virtual void Start()
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
        if(enemyWeapon != null)
        {
            enemyWeapon.onEnter += HandleEnemyEnter;
            enemyWeapon.onExit += HandleEnemyExit;
        }
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
    protected virtual void HandleEnemyEnter()
    {
        isEnemyAttackActive = true;
    }
    protected virtual void HandleEnemyExit()
    {
        isEnemyAttackActive = false;
    }
   
    protected virtual void OnDestroy()
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
        if(enemyWeapon != null)
        {
            enemyWeapon.onEnter -= HandleEnemyEnter;
            enemyWeapon.onExit -= HandleEnemyExit;
        }
    }
}
    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2: AttackData
    {
        protected T1 dataPartner;
        protected T1 dataPlayer;
        protected T1 dataEnemy;

        protected T2 currentAttackDataPartner;
        protected T2 currentAttackDataPlayer;
        protected T2 currentAttackDataEnemy;

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
    protected override void HandleEnemyEnter()
    {
        base.HandleEnemyEnter();
        if(enemyWeapon != null)
        {
            currentAttackDataEnemy = dataEnemy.AttackData[enemyWeapon.CurrentAttackCounter];
        }
    }
    

    public override void Init()
    {
        base.Init();
        if (partnerWeapon != null)
        {
            dataPartner = partnerWeapon.Data.GetData<T1>();
        }
        if(weapon != null)
        {
            dataPlayer = weapon.Data.GetData<T1>();

        }
        if(enemyWeapon != null)
        {
            Debug.Log(enemyWeapon.weaponData + "WeaponData");
            dataEnemy = enemyWeapon.weaponData.GetData<T1>();
        }
    }

    }


