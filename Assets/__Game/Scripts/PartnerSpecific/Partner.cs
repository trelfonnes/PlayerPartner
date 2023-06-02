using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : PlayableCharacters
{
    #region StateVariables for Specific Character

    public PlayerStateMachine StateMachine { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
     //   StateMachine.Initialize(FollowState); //for when states are referenced in awake.
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
