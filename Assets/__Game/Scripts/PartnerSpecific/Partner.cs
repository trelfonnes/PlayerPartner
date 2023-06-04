using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : PlayableCharacters
{
    #region StateVariables for Specific Character

    public PlayerStateMachine StateMachine { get; private set; }
    public PartnerMoveState MoveState { get; private set; }
    public PartnerIdleState IdleState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();
        playerDirection = Vector2.down;
        MoveState = new PartnerMoveState(this, StateMachine, playerSOData, _playerData, "move");
        IdleState = new PartnerIdleState(this, StateMachine, playerSOData, _playerData, "idle");
    }
    protected override void Start()
    {
        base.Start();
      StateMachine.InitializePartner(IdleState); //for when states are referenced in awake.
    }
    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentPartnerState.LogicUpdate();

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        StateMachine.CurrentPartnerState.PhysicsUpdate();
    }

    #region For Saving Data BIND
    internal void Bind(PlayerData playerData)
    {
        _playerData = playerData;
        Debug.Log("binding");
    }
    #endregion
}
