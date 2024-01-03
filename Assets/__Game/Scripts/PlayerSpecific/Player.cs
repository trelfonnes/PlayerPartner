using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour

{
    #region StateVariables for Specific Character
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }
    public PlayerSpecialState SpecialState { get; private set; }
    public PlayerCarryItemState CarryItemState { get; private set; }
    public PlayerHoldItemState HoldItemState { get; private set; }
    public PlayerWatchState WatchState { get; private set; }
    public PlayerEvolutionState EvolutionState { get; private set; }
    public PlayerDefeatedState DefeatedState { get; private set; }
    public PlayerFallingState FallingState { get; private set; }
    #endregion
    public CoreHandler core { get; private set; }
    public Animator anim { get; private set; }
    public CinemachineVirtualCamera PlayerCamera
    {
        get { return playerCamera; }
        set { playerCamera = value; }
    }

    public PlayerInputHandler InputHandler { get; private set; }
    protected PlayerData _playerData; //data for stats refactor might not need it here
    [SerializeField] public StatEvents statEvents;// TODO make protected and add to constructor of player state
    [SerializeField]
    public EvolutionEvents evolutionEvents;
    [SerializeField]
    protected PlayerSOData playerSOData;//Data for states  
    [SerializeField] CinemachineVirtualCamera playerCamera;
   
    public Vector2 playerDirection;
    public Vector2 lastDirection;
    Weapon primaryWeapon;
    Weapon secondaryWeapon;

    public event Action onFallOver;
    public event Action onFallStarted;


    #region Unity Callback Functions Initialized in Awake Method

    protected virtual void Awake()
    {
        
        playerDirection = Vector2.down;
        core = GetComponentInChildren<CoreHandler>();
        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
        primaryWeapon.SetCore(core);
        secondaryWeapon.SetCore(core);
        _playerData = PlayerData.Instance;
        anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerSOData, _playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerSOData, _playerData, "move");
        PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerSOData, _playerData, "attack", primaryWeapon, CombatInputs.primary);
        SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerSOData, _playerData, "attack", secondaryWeapon, CombatInputs.secondary);
        SpecialState = new PlayerSpecialState(this, StateMachine, playerSOData, _playerData, "special");
        CarryItemState = new PlayerCarryItemState(this, StateMachine, playerSOData, _playerData, "carryItem");
        HoldItemState = new PlayerHoldItemState(this, StateMachine, playerSOData, _playerData, "holdItem");
        WatchState = new PlayerWatchState(this, StateMachine, playerSOData, _playerData, "watch");
        EvolutionState = new PlayerEvolutionState(this, StateMachine, playerSOData, _playerData, "evolve");
        DefeatedState = new PlayerDefeatedState(this, StateMachine, playerSOData, _playerData, "defeated");
        FallingState = new PlayerFallingState(this, StateMachine, playerSOData, _playerData, "falling");

    }
    #endregion
    protected virtual void Start()
    {
      
        StateMachine.Initialize(IdleState); //for when states are referenced in awake.
    }
    protected virtual void Update()
    {
       
        //connected with logic update in playerstate for specific character
        StateMachine.CurrentState.LogicUpdate();
        
    }
    protected virtual void FixedUpdate()
    {
        
        //connected physics update for specific character
       StateMachine.CurrentState.PhysicsUpdate();
    }
    private void OnDisable()
    {
        StateMachine.CurrentState.OnDisable();
    }
    public void PlayerIsDefeated()
    {
        //logic for whatever needs to be done. Trigger game over screen
    }

    public void OnFallAnimEvent()
    {
        onFallOver?.Invoke();
    }
    public void OnStartFallEvent()
    {
        onFallStarted?.Invoke();
    }
    void UnsubscribeToEvolutionEvents()
    {

    }

    #region For Saving Data BIND
    internal void Bind(PlayerData playerData)
    {
        _playerData = playerData;
    }
    
    #endregion 
}
