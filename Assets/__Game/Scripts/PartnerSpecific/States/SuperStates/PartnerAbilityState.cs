using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerAbilityState : PartnerState
{
    protected bool isAbilityDone;
    protected bool switchInput;
    protected bool isTouchingPitfall;
    protected PartnerCollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private PartnerCollisionSenses collisionSenses;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
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
        isAbilityDone = false;
       
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        switchInput = partner.InputHandler.SwitchPlayerInput;


        if (isAbilityDone)
         {
            if (Movement?.CurrentVelocity.x < .01 && Movement?.CurrentVelocity.y < .01)
            {
                PSM.ChangePartnerState(partner.IdleState);
            }
         }
      
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
  
}
