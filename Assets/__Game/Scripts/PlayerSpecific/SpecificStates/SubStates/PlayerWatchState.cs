using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWatchState : PlayerBasicState
{
    private Movement movement;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    public PlayerWatchState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        isWatching = true;
        canExitState = false;
        Movement?.SetVelocity(playerSOData.watchSpeed * (new Vector2(xInput, yInput)));
        player.evolutionEvents.OnSwitchToPlayer += BackToIdle;

        
    }

   

    public override void Exit()
    {
        base.Exit();
        player.evolutionEvents.OnSwitchToPlayer -= BackToIdle;
        isWatching = false;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();//Event where camera already has access to player
                           //TODO logic for switching camera from player to partner.. perhaps on entry
                           // Within partner: logic for switching camera from partner to player. or switch back to player on exit.

        Debug.Log("In watch state");
        
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void BackToIdle()
    {
        PSM.ChangeState(player.IdleState);
    }
}
