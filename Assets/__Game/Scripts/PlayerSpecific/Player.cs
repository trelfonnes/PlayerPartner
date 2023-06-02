using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayableCharacters
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
    #region Unity Callback Functions Initialized in Awake Method
    protected override void Awake()
    {base.Awake();
        playerDirection = Vector2.down;
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
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState); //for when states are referenced in awake.
    }
    protected override void Update()
    {
        base.Update();
        //connected with logic update in playerstate for specific character
        StateMachine.CurrentState.LogicUpdate();
        
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //connected physics update for specific character
       StateMachine.CurrentState.PhysicsUpdate();
    }

    #region For Saving Data BIND
    internal void Bind(PlayerData playerData)
    {
        _playerData = playerData;
        Debug.Log("binding");
    }
    #endregion 
}
