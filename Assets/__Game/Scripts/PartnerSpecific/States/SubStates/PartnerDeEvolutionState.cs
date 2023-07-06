using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerDeEvolutionState : PartnerBasicState
{
    public bool isDevolving = true;

    public PartnerDeEvolutionState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
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
        isDevolving = true;
        Movement?.SetVelocity(playerSOData.watchSpeed * (new Vector2(1, 1)));
        partner.evolutionEvents.OnDevolve += DevolveOver;

    }

    public override void Exit()
    {
        base.Exit();
        isDevolvingAbilityCancel = false;
        partner.evolutionEvents.OnDevolve -= DevolveOver;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (!isDevolving)
        {
                PSM.ChangePartnerState(partner.FollowIdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    void DevolveOver(EvolutionEvents.EvolutionEventData e)
    {
        isDevolving = e.isDevolving;
    }

}
