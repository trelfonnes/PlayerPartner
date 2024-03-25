using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBasicState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    public PlayerFallingState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
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

    public override void Enter()
    {
        base.Enter();
        player.onFallOver += FallIsOver;
        AudioManager.Instance.PlayAudioClip("Falling");

        Debug.Log("Entered falling state");
    }

    public override void Exit()
    {
        base.Exit();
        player.onFallOver -= FallIsOver;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityZero();
        

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void FallIsOver()
    {
        PSM.ChangeState(player.IdleState);
    }
}
