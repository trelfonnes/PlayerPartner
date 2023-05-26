using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBasicState
{
    public PlayerMoveState(Player player, PlayerStateMachine PSM, PlayerBasicData playerData, string animBoolName) : base(player, PSM, playerData, animBoolName)
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
        Movement?.SetVelocity(playerData.moveSpeed * (new Vector2(xInput,  yInput).normalized));
       if(Movement.CurrentVelocity != Vector2.zero)
        {
            player.anim.SetFloat("moveY", Movement.CurrentVelocity.y);
            player.anim.SetFloat("moveX", Movement.CurrentVelocity.x);
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
