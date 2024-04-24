using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WeaponComponent : MonoBehaviour
{
    protected Weapon weapon;
    protected PartnerWeapon partnerWeapon;
    protected EnemyWeapon enemyWeapon;
    protected BossWeapon bossWeapon;
    protected Enemy enemy;
    protected Partner partner;
    protected Player player;
    protected BossAI boss;
    protected bool isAttackActive;
    protected bool isPartnerAttackActive;
    protected bool isEnemyAttackActive;
    protected bool isBossAttackActive;

    // protected AnimationEventHandler PlayerEventHandler => weapon.EventHandler;
    // protected AnimationEventHandler PartnerEventHandler => partnerWeapon.EventHandler;

    //TODO: switch these below to top and remove from awake Once ready.

    protected AnimationEventHandler PartnerEventHandler;
    protected AnimationEventHandler PlayerEventHandler;
    protected AnimationEventHandler EnemyEventHandler;
    protected AnimationEventHandler BossEventHandler;
    protected CoreHandler PlayerCore => weapon?.Core;
    protected CoreHandler PartnerCore => partnerWeapon?.Core;
    protected CoreHandler EnemyCore => enemyWeapon?.Core;
    protected BossComponentLocator BossCompLoc => bossWeapon?.ComponentLocator;

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

        BossEventHandler = GetComponentInChildren<AnimationEventHandler>();
        bossWeapon = GetComponent<BossWeapon>();
        boss = GetComponentInParent<BossAI>();
       

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
        if(bossWeapon != null)
        {
            bossWeapon.onEnter += HandleBossEnter;
            bossWeapon.onExit += HandleBossExit;
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
    protected virtual void HandleBossEnter()
    {
        isBossAttackActive = true;
    }
    protected virtual void HandleBossExit()
    {
        isBossAttackActive = false;
    }
   
    protected virtual void OnDestroy()
    {
        Debug.Log("OnDestroy is called from the weapon component. Events unsubbed");
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
        if(bossWeapon != null)
        {
            bossWeapon.onEnter -= HandleBossEnter;
            bossWeapon.onExit -= HandleBossExit;
        }
    }
}
    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2: AttackData
    {
        protected T1 dataPartner;
        protected T1 dataPlayer;
        protected T1 dataEnemy;
        protected T1 dataBoss;

        protected T2 currentAttackDataPartner;
        protected T2 currentAttackDataPlayer;
        protected T2 currentAttackDataEnemy;
        protected T2 currentAttackDataBoss;
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
    protected override void HandleBossEnter()
    {
        base.HandleBossEnter();
        if(bossWeapon != null)
        {
            currentAttackDataBoss = dataBoss.AttackData[bossWeapon.CurrentAttackCounter];
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
            dataEnemy = enemyWeapon.weaponData.GetData<T1>();
        }
        if(bossWeapon != null)
        {
            dataBoss = bossWeapon.weaponData.GetData<T1>();
        }
    }

    }


