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
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero += TimeToDevolve;

        }
    }

    public override void Exit()
    {
        base.Exit();
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero -= TimeToDevolve;

        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.CheckIfShouldFlip(xInput, yInput);
        Movement?.SetVelocity(playerSOData.moveSpeed * (new Vector2(xInput, yInput).normalized));
        if (Movement.CurrentVelocity != Vector2.zero)
        {
            Movement?.SetLatestVelocity(Movement.CurrentVelocity);
            partner.playerDirection = Movement.CurrentVelocity;
            partner.anim.SetFloat("moveY", partner.playerDirection.y);
            partner.anim.SetFloat("moveX", partner.playerDirection.x);
            partner.lastDirection = partner.playerDirection;
        }
        if (!isExitingState)
        {
            if(xInput == 0 && yInput == 0)
            {
                PSM.ChangePartnerState(partner.IdleState);
            }
        }
        if (primaryAttackInput)
        {
            PSM.ChangePartnerState(partner.PrimaryAttackState);
        }
        else if(xInput !=0 && yInput != 0 && primaryAttackInput)
        {
            Debug.Log("change to attack state");
            PSM.ChangePartnerState(partner.PrimaryAttackState);
        }
        if (secondaryAttackInput)
        {
            PSM.ChangePartnerState(partner.SecondaryAttackState);

        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
