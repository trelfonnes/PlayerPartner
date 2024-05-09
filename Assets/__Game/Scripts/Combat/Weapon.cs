using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    [SerializeField] bool isPrimaryWeapon;
    [SerializeField] WeaponAutoGenerator thisWeaponsAutoGenerator;
    [SerializeField] WeaponInventoryManager weaponInventoryManager;

    [SerializeField] WeaponDataSO BareHands;
    [SerializeField] WeaponDataSO BareHandsProjectile;
    [SerializeField] WeaponDataSO CableCord;
    [SerializeField] WeaponDataSO CableCord2;
    [SerializeField] WeaponDataSO CableCord3;
    [SerializeField] WeaponDataSO Shield;
    [SerializeField] WeaponDataSO Dart;
    [SerializeField] WeaponDataSO Boomerang;
    [SerializeField] WeaponDataSO Bomb;

   

    public WeaponDataSO Data { get; private set; }

    [SerializeField]  float attackCounterResetCooldown;
     public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
        
    }
   
    public event Action onExit;
    public event Action onEnter;
    public event Action<bool> OncurrentInputChange;
    Animator anim;
    public GameObject BaseGO { get; private set; }
    public GameObject WeaponSpriteGO { get; private set; }
    Player player;
    public AnimationEventHandler EventHandler { get; private set; }
    public CoreHandler Core { get; private set; }

    Movement playerCoreMovement;
    Movement PlayerCoreMovement
    {
        get => playerCoreMovement ??= Core.GetCoreComponent<Movement>();

    }

    int currentAttackCounter;
     private Timer attackCounterResetTimer;
    private bool currentInput;
    private Vector2 lastFacingCombatDirection;

    public bool CurrentInput
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
        print($"{transform.name} enter");
        anim.SetBool("active", true);
        anim.SetFloat("moveX", PlayerCoreMovement.facingCombatDirectionX);
        anim.SetFloat("moveY", PlayerCoreMovement.facingCombatDirectionY);
         anim.SetInteger("counter", CurrentAttackCounter);
        attackCounterResetTimer.StopTimer();
        onEnter?.Invoke();

    }

   public void Exit()
    {
        anim.SetBool("active", false);
        CurrentAttackCounter++;
        attackCounterResetTimer.StartTimer();
        onExit?.Invoke();
    }

    private void Awake()
    {
        BaseGO = transform.Find("Base").gameObject;
        WeaponSpriteGO = transform.Find("WeaponSprite").gameObject;
        player = GetComponentInParent<Player>();
        anim = BaseGO.GetComponent<Animator>();
        EventHandler = BaseGO.GetComponent<AnimationEventHandler>();
         attackCounterResetTimer = new Timer(attackCounterResetCooldown);
        
    }
    private void Update()
    {
        attackCounterResetTimer.Tick();
    }

    void ResetAttackCounter()
    {
        CurrentAttackCounter = 0;
    }
    private void Start()
    {
        SetWeaponAfterLoad();
    }
    void SwapWeaponToLastEquipped(string weapon)
    {
        if (weapon == "Boomerang")
        {
            thisWeaponsAutoGenerator.GenerateWeapon(Boomerang);
        }
         if (weapon == "BareHands")
        {
            thisWeaponsAutoGenerator.GenerateWeapon(BareHands);
        }
         if (weapon == "CableCord")
        {
            thisWeaponsAutoGenerator.GenerateWeapon(CableCord);
        }
         if (weapon == "CableCord2")
        {
            thisWeaponsAutoGenerator.GenerateWeapon(CableCord2);
        }

          if (weapon == "CableCord3")
        {
            thisWeaponsAutoGenerator.GenerateWeapon(CableCord3);
        }
        
        if (weapon == "Shield")
        {
            thisWeaponsAutoGenerator.GenerateWeapon(Shield);
        }
           
        if (weapon == "Bomb")
        {
            thisWeaponsAutoGenerator.GenerateWeapon(Bomb);
        }
          
        if (weapon == "Dart")
        {
            thisWeaponsAutoGenerator.GenerateWeapon(Dart);
        }

        
    }
    private void SwapWeapons()
    {
        if (Boomerang != null)
        {
            if (weaponInventoryManager.currentWeapon.weaponName == "Boomerang")
            {
                //generateboomerang
                thisWeaponsAutoGenerator.GenerateWeapon(Boomerang);

            }
        } if (BareHands != null)
        {
            if (weaponInventoryManager.currentWeapon.weaponName == "BareHands")
            {
                //generateboomerang
                thisWeaponsAutoGenerator.GenerateWeapon(BareHands);

            }
        }
        if (CableCord != null)
        {
            if (weaponInventoryManager.currentWeapon.weaponName == "CableCord")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(CableCord);
            }
        }  
        if (CableCord2 != null)
        {
            if (weaponInventoryManager.currentWeapon.weaponName == "CableCord2")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(CableCord2);
            }
        }  
        if (CableCord3 != null)
        {
            if (weaponInventoryManager.currentWeapon.weaponName == "CableCord3")
            {

                thisWeaponsAutoGenerator.GenerateWeapon(CableCord3);
            }
        }
        if (Shield != null)
        {
            if (weaponInventoryManager.currentWeapon.weaponName == "Shield")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(Shield);

            }
        }
        if (Bomb != null)
        {
            if (weaponInventoryManager.currentWeapon.weaponName == "Bomb")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(Bomb);

            }
        }
        if (Dart != null)
        {
            if (weaponInventoryManager.currentWeapon.weaponName == "Dart")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(Dart);

            }
        }



    }
    private void OnEnable()
    {
        weaponInventoryManager.onPlayerWeaponSwapped += SwapWeapons;
        EventHandler.OnFinish += Exit;
         attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        weaponInventoryManager.LoadCurrentEquippedWeapon();
        

    }
    private void OnDisable()
    {
        weaponInventoryManager.onPlayerWeaponSwapped -= SwapWeapons;
        EventHandler.OnFinish -= Exit;
         attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;

    }

    void SetWeaponAfterLoad()
    {
        WeaponInventoryItemSO primaryWeapon = weaponInventoryManager.GetPrimarySavedEquippedWeapon();
        WeaponInventoryItemSO secondaryWeapon = weaponInventoryManager.GetSecondarySavedEquippedWeapon();

        if (isPrimaryWeapon && primaryWeapon)
        {
            SwapWeaponToLastEquipped(primaryWeapon.weaponName);
        }
        if (!isPrimaryWeapon && secondaryWeapon)
        {
            SwapWeaponToLastEquipped(secondaryWeapon.weaponName);
        }
    }

    public void SetCore(CoreHandler core)
    {
        Core = core;
    }
    public void SetData(WeaponDataSO data)
    {
        Data = data;
    }
 
}
