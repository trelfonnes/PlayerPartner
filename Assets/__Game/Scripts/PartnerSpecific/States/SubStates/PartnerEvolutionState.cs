using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerEvolutionState : PartnerFollowState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
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
        partner.evolutionEvents.OnEvolveToThirdStage += EvolveCheck;
        partner.evolutionEvents.OnEvolveToSecondStage += EvolveCheck;
        Subscribe((handler) => statEvents.onCurrentHealthZero += handler, Partner1Defeated);


    }



    public override void Exit()
    {
        base.Exit();
        
        partner.evolutionEvents.OnEvolveToThirdStage -= EvolveCheck;
        partner.evolutionEvents.OnEvolveToSecondStage -= EvolveCheck;
        statEvents.onCurrentHealthZero -= Partner1Defeated;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        partner.evolutionEvents.OnEvolveToThirdStage -= EvolveCheck;
        partner.evolutionEvents.OnEvolveToSecondStage -= EvolveCheck;
        statEvents.onCurrentHealthZero -= Partner1Defeated;
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

    void EvolveCheck( EvolutionEvents.EvolutionEventData e)
    {
        isEvolving = e.isEvolving;

    }
}
