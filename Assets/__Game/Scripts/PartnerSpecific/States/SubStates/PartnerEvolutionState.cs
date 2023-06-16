using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerEvolutionState : PartnerBasicState
{
    private bool isEvolving;
    public PartnerEvolutionState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
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
        isEvolving = true;
        Movement?.SetVelocity(playerSOData.watchSpeed * (new Vector2(1, 1)));
        EvolveBehavior.OnEndEvolution += delegate (object sender, EvolveBehavior.OnEvolutionEventArgs e)
        {
           isEvolving = e.isEvolving;
        };

    }



    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isEvolving)
        {
            PSM.ChangePartnerState(partner.FollowIdleState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
