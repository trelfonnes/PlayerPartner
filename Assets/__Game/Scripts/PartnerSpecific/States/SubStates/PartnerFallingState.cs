using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerFallingState : PartnerBasicState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    public PartnerFallingState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
    }

   
    public override void Enter()
    {
        base.Enter();
        Debug.Log("partner entered falling state");

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityZero();

        if (!isTouchingPitfall)
        {
            PSM.ChangePartnerState(partner.IdleState);
        }


    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

}
