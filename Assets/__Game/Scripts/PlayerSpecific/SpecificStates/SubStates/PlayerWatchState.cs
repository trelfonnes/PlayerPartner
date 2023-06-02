using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWatchState : PlayerBasicState
{
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
        Debug.Log("enteredWatch");
        canExitState = false;
        Movement?.SetVelocity(playerSOData.watchSpeed * (new Vector2(xInput, yInput)));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();//Event where camera already has access to player
        //TODO logic for switching camera from player to partner.. perhaps on entry
        // Within partner: logic for switching camera from partner to player. or switch back to player on exit.
        if (!switchInput)
        {
            canExitState = true;
        }
        if (canExitState)
        {
            if (switchInput)
            {
                PSM.ChangeState(player.IdleState);
            }
        }
        
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}