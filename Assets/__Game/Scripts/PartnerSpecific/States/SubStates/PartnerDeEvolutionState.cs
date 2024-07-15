using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerDeEvolutionState : PartnerBasicState
{
    public bool isDevolving = true;
    CoreHandler Core;
     Movement Movement { get => movement ?? Core.GetCoreComponent(ref movement); }
    private Movement movement;
    public PartnerDeEvolutionState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
        Core = partner.core;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

  

    public override void Enter()
    {
        base.Enter();
        isDevolving = true;
        partner.evolutionEvents.OnDevolve += DevolveOver;
        Subscribe((handler) => statEvents.onCurrentHealthZero += handler, Partner1Defeated);


    }

    public override void Exit()
    {
        base.Exit();
        isDevolvingAbilityCancel = false;
        partner.evolutionEvents.OnDevolve -= DevolveOver;
        statEvents.onCurrentHealthZero -= Partner1Defeated;

    }
    public override void OnDisable()
    {
        base.OnDisable();
        partner.evolutionEvents.OnDevolve -= DevolveOver;
        statEvents.onCurrentHealthZero -= Partner1Defeated;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement.SetVelocity(playerSOData.watchSpeed * (new Vector2(1, 1)));


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
