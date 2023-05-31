using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBasicState
{
    public PlayerMoveState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Movement?.CheckIfShouldFlip(xInput, yInput);
        Movement?.SetVelocity(playerSOData.moveSpeed * (new Vector2(xInput,  yInput).normalized));
       if(Movement.CurrentVelocity != Vector2.zero)
        {
            player.playerDirection = Movement.CurrentVelocity;
            player.anim.SetFloat("moveY", player.playerDirection.y);
            player.anim.SetFloat("moveX", player.playerDirection.x);
        }

        if (!isExitingState)
        {
            
            if (xInput == 0 && yInput == 0)
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
