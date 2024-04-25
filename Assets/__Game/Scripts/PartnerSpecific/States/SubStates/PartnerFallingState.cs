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
       // partner.onFallOver += FallIsOver;
        Subscribe((handler) => partner.onFallOver += handler, FallIsOver);

        AudioManager.Instance.PlayAudioClip("Falling");

        Debug.Log("partner entered falling state");
        Subscribe((handler) => statEvents.onCurrentHealthZero += handler, Partner1Defeated);

    }

    public override void Exit()
    {
        base.Exit();
        partner.onFallOver -= FallIsOver;
        statEvents.onCurrentHealthZero -= Partner1Defeated;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        partner.onFallOver -= FallIsOver;
        statEvents.onCurrentHealthZero -= Partner1Defeated;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityZero();

        


    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
    public void FallIsOver()
    {
        PSM.ChangePartnerState(partner.IdleState);
    }
}

