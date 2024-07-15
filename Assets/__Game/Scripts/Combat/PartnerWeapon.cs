using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PartnerWeapon : MonoBehaviour
{
    [SerializeField] float attackCounterResetCooldown;

    [SerializeField] WeaponAutoGenerator thisWeaponsAutoGenerator;
    [SerializeField] WeaponInventoryManager weaponInventoryManager;
    [SerializeField] WeaponDataSO MeleeBasic;
    [SerializeField] WeaponDataSO MeleeHold;
    [SerializeField] WeaponDataSO BasicProjectile;
    [SerializeField] WeaponDataSO ChargeProjectile;
    [SerializeField] WeaponDataSO SpreadProjectile;
    private static bool isAnyInstanceAttacking = false;
  
   // PartnerWeaponState partnerWeaponStateInstance;
    public WeaponDataSO Data { get; private set; }
    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;

    }

    public event Action onExit;
    public event Action onEnter;
    public event Action onDevolve;
    public event Action<bool> OncurrentInputChange;
    Animator anim;
    public GameObject BaseGO { get; private set; }
    public GameObject WeaponSpriteGO { get; private set; }

    Partner partner;
    public AnimationEventHandler EventHandler { get; private set; }
    public CoreHandler Core { get; private set; }
    Movement partnerCoreMovement;
    Movement PartnerCoreMovement
    {
        get => partnerCoreMovement ??= Core.GetCoreComponent<Movement>();

    }
    bool devolve;
    bool currentInput;
    int currentAttackCounter;
    private Timer attackCounterResetTimer;
    
    public bool CurrentInput // get this from player attack state in logic update
    {
        get => currentInput;
        set
        {
            if(currentInput != value)
            {
                currentInput = value;
                OncurrentInputChange?.Invoke(currentInput);
            }
        }
    }
    public void Enter()
    {
        anim.SetBool("active", true);
        anim.SetFloat("moveX", PartnerCoreMovement.facingCombatDirectionX);
        anim.SetFloat("moveY", PartnerCoreMovement.facingCombatDirectionY);
        anim.SetInteger("counter", CurrentAttackCounter);
        attackCounterResetTimer.StopTimer();
        isAnyInstanceAttacking = true;
        onEnter?.Invoke();
    }
    public void Exit()
    {
        anim.SetBool("active", false);
        CurrentAttackCounter++;
        attackCounterResetTimer.StartTimer();
        isAnyInstanceAttacking = false;
        onExit?.Invoke();
    }
    public static bool AnyInstanceAttacking()
    {
        
        return isAnyInstanceAttacking;
    }
    private void Awake()
    {
        BaseGO = transform.Find("Base").gameObject;
        WeaponSpriteGO = transform.Find("WeaponSprite").gameObject;
        partner = GetComponentInParent<Partner>();
        anim = BaseGO.GetComponent<Animator>();
        EventHandler = BaseGO.GetComponent<AnimationEventHandler>();
        attackCounterResetTimer = new Timer(attackCounterResetCooldown);
     //   partnerWeaponStateInstance = PartnerWeaponState.GetInstance();


    }
    private void Start()
    {
        CheckWeaponEquippedState();
        SetWeaponAfterLoad();
    }
    private void Update()
    {
        attackCounterResetTimer.Tick();
    }
    void ResetAttackCounter()
    {
        CurrentAttackCounter = 0;
    }
    void SwapWeapons()
    {
        Debug.Log("Swap weapon to from parther weapon");

        //check the state of the projectile
        PrimaryWeaponState currentPrimaryWeapon = PartnerWeaponState.Instance.GetCurrentPrimaryState();
        //PrimaryWeaponState currentPrimaryWeapon = partnerWeaponStateInstance.GetCurrentPrimaryState();
        SecondaryWeaponState currentSecondaryWeapon = PartnerWeaponState.Instance.GetCurrentSecondaryState();
 //       SecondaryWeaponState currentSecondaryWeapon = partnerWeaponStateInstance.GetCurrentSecondaryState();
        if (MeleeBasic != null)
        {
            if (currentPrimaryWeapon == PrimaryWeaponState.MeleeBasic)
            {
                thisWeaponsAutoGenerator.GenerateWeapon(MeleeBasic);
            }
            if (currentPrimaryWeapon == PrimaryWeaponState.MeleeHold)
            {
                thisWeaponsAutoGenerator.GenerateWeapon(MeleeHold);
            }
        }
        if (BasicProjectile != null)
        {
            if (currentSecondaryWeapon == SecondaryWeaponState.BasicProjectile)
            {
                thisWeaponsAutoGenerator.GenerateWeapon(BasicProjectile);
            }
            if (currentSecondaryWeapon == SecondaryWeaponState.ChargeProjectile)
            {
                thisWeaponsAutoGenerator.GenerateWeapon(ChargeProjectile);
            }
            if (currentSecondaryWeapon == SecondaryWeaponState.SpreadProjectile)
            {
                thisWeaponsAutoGenerator.GenerateWeapon(SpreadProjectile);
            }
        }
        else
            return;


    }

    void SetWeaponAfterLoad()
    {
        WeaponInventoryItemSO primaryWeapon = weaponInventoryManager.GetPartnerPrimarySavedEquippedWeapon();
        WeaponInventoryItemSO secondaryWeapon = weaponInventoryManager.GetPartnerSecondarySavedEquippedWeapon();

        if (primaryWeapon)
        {
            SwapWeaponToLastEquipped(primaryWeapon.weaponName);
        }
        if (secondaryWeapon)
        {
            SwapWeaponToLastEquipped(secondaryWeapon.weaponName);
        }
    }
    void SwapWeaponToLastEquipped(string weaponName)
    {
        if (MeleeBasic)
        {
            if (weaponName == "Melee")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(MeleeBasic);

            }
        }
        if (MeleeHold)
        {
            if (weaponName == "Elemental")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(MeleeHold);

            }
        }
        if (BasicProjectile)
        {
            if (weaponName == "Basic")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(BasicProjectile);

            }
        }
        if (ChargeProjectile)
        {
            if (weaponName == "Charged")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(ChargeProjectile);

            }
        }
        if (SpreadProjectile)
        {
            if (weaponName == "Spread")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(SpreadProjectile);

            }
        }

    }
    private void OnEnable()
    {
        weaponInventoryManager.onPartnerWeaponSwapped += SwapWeapons;
        EventHandler.OnFinish += Exit;
        partner.statEvents.onCurrentEPZero += Devolve;
        attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        CheckWeaponEquippedState();

    }

    

    private void OnDisable()
    {
        weaponInventoryManager.onPartnerWeaponSwapped -= SwapWeapons;
        EventHandler.OnFinish -= Exit;
        partner.statEvents.onCurrentEPZero -= Devolve;

        attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;

    }
    void Devolve()
    {
        anim.SetBool("attack", false);
        attackCounterResetTimer.StartTimer();
        onDevolve.Invoke();

    }
    public void SetCore(CoreHandler core)
    {
        Core = core;
    }
    public void SetData(WeaponDataSO data)
    {
        Data = data;
    }
    public void CheckWeaponEquippedState()
    {
        PrimaryWeaponState currentPrimaryState = PartnerWeaponState.Instance.GetCurrentPrimaryState();
        SecondaryWeaponState currentSecondaryState = PartnerWeaponState.Instance.GetCurrentSecondaryState();
        if (MeleeBasic != null)
        {
            if (currentPrimaryState == PrimaryWeaponState.MeleeBasic)
            {

                thisWeaponsAutoGenerator.GenerateWeapon(MeleeBasic);
                //generate weapon with Melee BasicData

            }
            if (currentPrimaryState == PrimaryWeaponState.MeleeHold)
            {
                thisWeaponsAutoGenerator.GenerateWeapon(MeleeHold);
            }
        }
        if (BasicProjectile != null)
        {
            if (currentSecondaryState == SecondaryWeaponState.BasicProjectile)
            {
                thisWeaponsAutoGenerator.GenerateWeapon(BasicProjectile);
            }
            if (currentSecondaryState == SecondaryWeaponState.ChargeProjectile)
            {
                thisWeaponsAutoGenerator.GenerateWeapon(ChargeProjectile);
            }
            if (currentSecondaryState == SecondaryWeaponState.SpreadProjectile)
            {
                thisWeaponsAutoGenerator.GenerateWeapon(SpreadProjectile);
            }
        }
        else
            return;
        //add more if needed.
       
        //weapon data for players is sent via object to pick up in 3's(one for each partner)
        // those 3 are somehow contianed together and stored for reference.


    }
}
