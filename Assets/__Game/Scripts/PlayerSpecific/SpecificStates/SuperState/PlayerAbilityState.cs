using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    private Movement movement;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    protected bool isAbilityDone;
    protected PlayerCollisionSenses PlayerCollisionSenses { get => playerCollisionSenses ?? core.GetCoreComponent(ref playerCollisionSenses); }
    private PlayerCollisionSenses playerCollisionSenses;
    public PlayerAbilityState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
    {
    }

   
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAbilityDone)
        {
            if(Movement?.CurrentVelocity.x < .01 && Movement?.CurrentVelocity.y < .01)
            {
                PSM.ChangeState(player.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
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
