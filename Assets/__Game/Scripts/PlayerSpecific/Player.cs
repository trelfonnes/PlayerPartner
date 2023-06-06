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
    public PlayerAttackState AttackState { get; private set; }
    public PlayerSpecialState SpecialState { get; private set; }
    public PlayerCarryItemState CarryItemState { get; private set; }
    public PlayerHoldItemState HoldItemState { get; private set; }
    public PlayerWatchState WatchState { get; private set; }
    #endregion
    public CoreHandler core { get; private set; }
    public Animator anim { get; private set; }
    public CinemachineVirtualCamera PlayerCamera
    {
        get { return playerCamera; }
        set { playerCamera = value; }
    }

    public PlayerInputHandler InputHandler { get; private set; }
    protected PlayerData _playerData = new PlayerData(); //data for stats refactor might not need it here
    [SerializeField]
    protected PlayerSOData playerSOData;//Data for states  
    [SerializeField] CinemachineVirtualCamera playerCamera;

    public Vector2 playerDirection;




    #region Unity Callback Functions Initialized in Awake Method
    private void OnEnable()
    {
        CameraSwitcher.Register(playerCamera);
    }
    protected virtual void Awake()
    {
        
        playerDirection = Vector2.down;
        StateMachine = new PlayerStateMachine();
        core = GetComponentInChildren<CoreHandler>();
        IdleState = new PlayerIdleState(this, StateMachine, playerSOData, _playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerSOData, _playerData, "move");
        AttackState = new PlayerAttackState(this, StateMachine, playerSOData, _playerData, "attack");
        SpecialState = new PlayerSpecialState(this, StateMachine, playerSOData, _playerData, "special");
        CarryItemState = new PlayerCarryItemState(this, StateMachine, playerSOData, _playerData, "carryItem");
        HoldItemState = new PlayerHoldItemState(this, StateMachine, playerSOData, _playerData, "holdItem");
        WatchState = new PlayerWatchState(this, StateMachine, playerSOData, _playerData, "watch");


    }
    #endregion
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
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
        CameraSwitcher.UnRegister(playerCamera);
    }
    #region For Saving Data BIND
    internal void Bind(PlayerData playerData)
    {
        _playerData = playerData;
    }
    
    #endregion 
}
