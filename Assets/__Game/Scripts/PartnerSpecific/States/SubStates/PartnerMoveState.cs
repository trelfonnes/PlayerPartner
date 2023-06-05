using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerMoveState : PartnerBasicState
{
    public PartnerMoveState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
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
        Movement?.SetVelocity(playerSOData.moveSpeed * (new Vector2(xInput, yInput).normalized));
        if (Movement.CurrentVelocity != Vector2.zero)
        {
            partner.playerDirection = Movement.CurrentVelocity;
            partner.anim.SetFloat("moveY", partner.playerDirection.y);
            partner.anim.SetFloat("moveX", partner.playerDirection.x);
        }
        if (!isExitingState)
        {
            if(xInput == 0 && yInput == 0)
            {
                PSM.ChangePartnerState(partner.IdleState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}