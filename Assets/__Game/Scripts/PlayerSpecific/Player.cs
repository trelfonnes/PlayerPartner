using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region StateVariables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerSpecialState SpecialState { get; private set; }
    public PlayerCarryItemState CarryItemState { get; private set; }
    public PlayerHoldItemState HoldItemState { get; private set; }
    public PlayerWatchState WatchState { get; private set; }
    #endregion
    
    #region Components on Player GO

    public CoreHandler core { get; private set; }
    public Animator anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
   
    private PlayerData _playerData = new PlayerData(); //data for stats refactor might not need it here
    [SerializeField] 
    private PlayerSOData playerSOData;//Data for states  

    #endregion

    #region Unity Callback Functions Initialized in Awake Method

    private void Awake()
    {
        playerDirection = Vector2.down;
        core = GetComponentInChildren<CoreHandler>();
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerSOData, _playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerSOData, _playerData, "move");
        AttackState = new PlayerAttackState(this, StateMachine, playerSOData, _playerData, "attack");
        SpecialState = new PlayerSpecialState(this, StateMachine, playerSOData, _playerData, "special");
        CarryItemState = new PlayerCarryItemState(this, StateMachine, playerSOData, _playerData, "carryItem");
        HoldItemState = new PlayerHoldItemState(this, StateMachine, playerSOData, _playerData, "holdItem");
        WatchState = new PlayerWatchState(this, StateMachine, playerSOData, _playerData, "watch");   
    }
    #endregion
    #region CollisionCheckVariables
    public Vector2 playerDirection;
    #endregion

    
    void Start()
    {
        anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        StateMachine.Initialize(IdleState); //for when states are referenced in awake.
    }

    void Update()
    {
        //connected with logic update in playerstate
        StateMachine.CurrentState.LogicUpdate();
        
    }

    private void FixedUpdate()
    {
        //connected physics update
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region For Saving Data
    internal void Bind(PlayerData playerData)
    {
        _playerData = playerData;
        Debug.Log("binding");
    }
    #endregion
}
