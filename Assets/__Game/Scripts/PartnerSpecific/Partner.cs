using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Partner : MonoBehaviour
{
    #region StateVariables for Specific Character

    public PlayerStateMachine StateMachine { get; private set; }
    public PartnerMoveState MoveState { get; private set; }
    public PartnerIdleState IdleState { get; private set; }
    public PartnerFollowIdleState FollowIdleState {get; private set;}
    public PartnerFollowMoveState FollowMoveState { get; private set; }
    public PartnerEvolutionState EvolutionState { get; private set; }
    public PartnerDeEvolutionState DevolveState { get; private set; }
    public PartnerJumpState JumpState { get; private set; }
    public PartnerDashState DashState { get; private set; }
    public PartnerAttackState PrimaryAttackState { get; private set; }
    public PartnerAttackState SecondaryAttackState { get; private set; }
    public PartnerDefeatedState DefeatedState { get; private set; }
    public PartnerFallingState FallingState { get; private set; }
    #endregion
    public CoreHandler core { get; private set; }
    public Animator anim { get; private set; }
    public CinemachineVirtualCamera PartnerCamera
    {
        get { return partnerCamera; }
        set { partnerCamera = value; }
    }
   public Timer DashCooldownTimer;
   public Timer JumpCooldownTimer;

    public bool stageOne;
    public bool stageTwo;
    public bool stageThree;
    public bool initializeForBattleScene = false; //check this for partner instances in the battle scenes
    public float dashCooldown = 3f;
    public float jumpCooldown = 0f;
    public PlayerInputHandler InputHandler { get; private set; }
    protected PlayerData _playerData; //data for stats refactor might not need it here
    [SerializeField]
    public PlayerSOData playerSOData; 
    [SerializeField]
    public EvolutionEvents evolutionEvents;//can refactor to put these in constructor
    [SerializeField] public StatEvents statEvents;
    [SerializeField] CinemachineVirtualCamera partnerCamera;
    public Vector2 playerDirection;
    public Vector2 lastDirection;
    PartnerWeapon primaryWeapon;
    PartnerWeapon secondaryWeapon;
    public BoxCollider2D partnerCollider { get; private set; }

    public event Action onFallOver;
    public event Action onFallStarted;

    protected virtual void Awake()
    {
        core = GetComponentInChildren<CoreHandler>();
        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<PartnerWeapon>();
        secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<PartnerWeapon>();
        primaryWeapon.SetCore(core);
        secondaryWeapon.SetCore(core);
        anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        _playerData = PlayerData.Instance;
        StateMachine = new PlayerStateMachine();
        playerDirection = Vector2.down;
        partnerCollider = GetComponent<BoxCollider2D>();
        Debug.Log("Awake method of this Partner being initialized" + gameObject.name);
        PrimaryAttackState = new PartnerAttackState(this, StateMachine, playerSOData, _playerData, "attack", primaryWeapon, CombatInputs.primary);
        SecondaryAttackState = new PartnerAttackState(this, StateMachine, playerSOData, _playerData, "attack", secondaryWeapon, CombatInputs.secondary);
        MoveState = new PartnerMoveState(this, StateMachine, playerSOData, _playerData, "move");
        IdleState = new PartnerIdleState(this, StateMachine, playerSOData, _playerData, "idle");
        FollowIdleState = new PartnerFollowIdleState(this, StateMachine, playerSOData, _playerData, "followIdle");
        FollowMoveState = new PartnerFollowMoveState(this, StateMachine, playerSOData, _playerData, "followMove");
        EvolutionState = new PartnerEvolutionState(this, StateMachine, playerSOData, _playerData, "evolve");
        DevolveState = new PartnerDeEvolutionState(this, StateMachine, playerSOData, _playerData, "devolve");
        JumpState = new PartnerJumpState(this, StateMachine, playerSOData, _playerData, "jump");
        DashState = new PartnerDashState(this, StateMachine, playerSOData, _playerData, "dash");
        DefeatedState = new PartnerDefeatedState(this, StateMachine, playerSOData, _playerData, "defeated");
        FallingState = new PartnerFallingState(this, StateMachine, playerSOData, _playerData, "falling");
    }
    protected virtual void Start()
    {
        DashCooldownTimer = new Timer(dashCooldown);
        JumpCooldownTimer = new Timer(jumpCooldown);
        if (initializeForBattleScene)
        {
            StateMachine.InitializePartner(IdleState);
        }
        else
            StateMachine.InitializePartner(FollowIdleState); //for when states are referenced in awake.
       // 
    }
    private void OnEnable()
    {
        InputHandler.ChangeMuteInput(false);

     
            StateMachine.InitializePartner(FollowIdleState);
        
    }
    protected virtual void Update()
    {
        if (playerSOData.canDash)
        {
            DashCooldownTimer.Update(Time.deltaTime);
        }
        if (playerSOData.canJump)
        {
            JumpCooldownTimer.Update(Time.deltaTime);
        }
        StateMachine.CurrentPartnerState.LogicUpdate();
        

    }

   
    protected virtual void FixedUpdate()
    {
        
        StateMachine.CurrentPartnerState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        StateMachine.CurrentPartnerState.OnDisable();
    }

    #region PartnerEvolutionEventsForAnimEvents
    void EvolveToSecondStage()
    {
        evolutionEvents.EvolveToSecondStage();
    }

    void EvolveToThirdStage()
    {
        evolutionEvents.EvolveToFinalStage();
    }
    void StopForEvolution()
    {
        evolutionEvents.StopForEvolution();
    }
    void ReturnFromEvolution()
    {
        evolutionEvents.ReturnFromEvolution();
    }
    void Devolve()
    {
        evolutionEvents.Devolve();
    }


    #endregion


    public void OnFallAnimEvent()
    {
        onFallOver?.Invoke();
    }
    public void OnStartFallEvent()
    {
        onFallStarted?.Invoke();
        Debug.Log("InvokeFallStartedPartner");

    }



    #region For Saving Data BIND
    //TODO Implement a save system for writing SO Data
    internal void Bind(PlayerData playerData)
    {
        _playerData = playerData;
        Debug.Log("binding");
    }
    #endregion


}
