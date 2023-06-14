using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerFollowIdleState : PartnerFollowState
{
    public PartnerFollowIdleState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
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
        if(evolveInput && playerSOData.stage2 && playerSOData.EP >= 25)
        {
            PSM.ChangePartnerState(partner.EvolutionState);
            Debug.Log("StateToEvolve");
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
