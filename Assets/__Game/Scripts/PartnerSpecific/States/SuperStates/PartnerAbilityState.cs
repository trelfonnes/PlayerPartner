using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerAbilityState : PartnerState
{
    protected bool isAbilityDone;
    protected bool switchInput;


    public PartnerAbilityState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
    }
    
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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
        switchInput = partner.InputHandler.SwitchPlayerInput;


        //if (isAbilityDone)
        // {
        //    PSM.ChangePartnerState(partner.IdleState);
        // }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
