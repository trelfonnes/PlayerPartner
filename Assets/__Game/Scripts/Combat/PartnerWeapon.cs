using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PartnerWeapon : MonoBehaviour
{
    [SerializeField] float attackCounterResetCooldown;
    [field: SerializeField] public WeaponDataSO Data { get; private set; }
    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;

    }

    public event Action onExit;
    public event Action onEnter;
    public event Action onDevolve;
    Animator anim;
    public GameObject BaseGO { get; private set; }
    public GameObject WeaponSpriteGO { get; private set; }

    Partner partner;
    public AnimationEventHandler EventHandler { get; private set; }
    public CoreHandler Core { get; private set; }
    bool devolve;
    int currentAttackCounter;
    private Timer attackCounterResetTimer;
    public void Enter()
    {
        print($"{transform.name} enter");
        anim.SetBool("active", true);
        anim.SetFloat("moveX", partner.lastDirection.x);
        anim.SetFloat("moveY", partner.lastDirection.y);
        anim.SetInteger("counter", CurrentAttackCounter);
        attackCounterResetTimer.StopTimer();
        onEnter?.Invoke();
    }
    void Exit()
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
        partner = GetComponentInParent<Partner>();
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
    private void OnEnable()
    {
        EventHandler.OnFinish += Exit;
        partner.statEvents.onCurrentEPZero += Devolve;
        attackCounterResetTimer.OnTimerDone += ResetAttackCounter;

    }
    private void OnDisable()
    {
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
}
