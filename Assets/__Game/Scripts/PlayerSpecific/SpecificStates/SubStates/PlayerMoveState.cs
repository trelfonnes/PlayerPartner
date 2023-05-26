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
        Movement?.SetVelocity(playerData.moveSpeed * xInput, playerData.moveSpeed * yInput);
      
        if(!isExitingState)
        {
            if(xInput == 0 && yInput == 0)
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
