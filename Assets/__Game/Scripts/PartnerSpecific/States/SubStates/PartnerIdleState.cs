using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerIdleState : PartnerBasicState
{
    public PartnerIdleState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
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
        if (xInput != 0 || yInput != 0)
        {
            PSM.ChangePartnerState(partner.MoveState);
        }
        if(!switchInput && !interactInput)
        {
            canExitState = true;
        }
       
        if (canExitState)
        {
            if (switchInput)
            {
                PSM.ChangePartnerState(partner.FollowIdleState);
            }
            //TODO behavior to interact input conditions
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
