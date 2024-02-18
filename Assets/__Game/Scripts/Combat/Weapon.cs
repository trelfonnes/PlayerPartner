using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponAutoGenerator thisWeaponsAutoGenerator;
    [SerializeField] WeaponInventoryManager weaponInventoryManager;

    [SerializeField] WeaponDataSO BareHands;
    [SerializeField] WeaponDataSO BareHandsProjectile;
    [SerializeField] WeaponDataSO CableCord;
    [SerializeField] WeaponDataSO CableCord2;
    [SerializeField] WeaponDataSO CableCord3;
    [SerializeField] WeaponDataSO Scythe;
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
        if (Scythe != null)
        {
            if (weaponInventoryManager.currentWeapon.weaponName == "Scythe")
            {
                thisWeaponsAutoGenerator.GenerateWeapon(Scythe);

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
    }
    private void OnDisable()
    {
        weaponInventoryManager.onPlayerWeaponSwapped -= SwapWeapons;
        EventHandler.OnFinish -= Exit;
         attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;

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
