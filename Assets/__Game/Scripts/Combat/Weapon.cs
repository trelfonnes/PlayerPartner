using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int numberOfAttacks;
     [SerializeField]  float attackCounterResetCooldown;
     public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set
        {
            if (value >= numberOfAttacks)
            { currentAttackCounter = 0; }
            else { currentAttackCounter = value; }
        }
    }

    public event Action onExit;
    public event Action onEnter;
    Animator anim;
    public GameObject BaseGO { get; private set; }
    public GameObject WeaponSpriteGO { get; private set; }
    Player player;
    AnimationEventHandler eventHandler;

    int currentAttackCounter;
     private Timer attackCounterResetTimer;
  public void Enter()
    {
        print($"{transform.name} enter");
        anim.SetBool("active", true);
        anim.SetFloat("moveX", player.lastDirection.x);
        anim.SetFloat("moveY", player.lastDirection.y);
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
        player = GetComponentInParent<Player>();
        anim = BaseGO.GetComponent<Animator>();
        eventHandler = BaseGO.GetComponent<AnimationEventHandler>();
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
        eventHandler.OnFinish += Exit;
         attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
    }
    private void OnDisable()
    {
        eventHandler.OnFinish -= Exit;
         attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;

    }
}
