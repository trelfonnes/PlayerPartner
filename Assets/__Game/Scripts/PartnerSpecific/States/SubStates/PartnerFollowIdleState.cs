using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerFollowIdleState : PartnerFollowState
{
    public PartnerFollowIdleState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
        if (playerSOData.stage2)
        {
            statEvents.onCurrentEPZero2 += TimeToDevolve;

        }
        else if (playerSOData.stage3)
        {
            statEvents.onCurrentEPZero3 += TimeToDevolve;

        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        canExitState = false;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isTouchingPlayer && !isTouchingWallFollowing)
        {
            PSM.ChangePartnerState(partner.FollowMoveState);
        }
        if(!switchInput && !interactInput)
        {
            canExitState = true;
        }
        
        if (canExitState)
        {
            if (switchInput)
            {
                PSM.ChangePartnerState(partner.IdleState);
            }
        }
        if(evolveInput && isTouchingPlayer && !playerSOData.stage3 && playerData.ep >= 25f)
        {
            if (playerSOData.stage1 && playerData.deviceOneCollected)
            {
                PSM.ChangePartnerState(partner.EvolutionState);
            }
            else if(playerSOData.stage2 && playerData.deviceTwoCollected)
            {
                PSM.ChangePartnerState(partner.EvolutionState);

            }
        }
       

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
}
