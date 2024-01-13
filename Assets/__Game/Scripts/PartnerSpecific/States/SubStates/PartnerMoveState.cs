using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerMoveState : PartnerBasicState
{
    public PartnerMoveState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
    }
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    protected Particles Particles { get => particles ?? core.GetCoreComponent(ref particles); }
    private Particles particles;

    void LevelUp()
    {
        Particles.StartParticles(ParticleType.LevelUp, core.transform.position, core.transform.rotation);
    }

public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero += TimeToDevolve;

        }
        partner.onFallStarted += StartFalling;

        statEvents.onLevelUp += LevelUp;
    }

    public override void Exit()
    {
        base.Exit();
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero -= TimeToDevolve;

        }
        partner.onFallStarted -= StartFalling;

        statEvents.onLevelUp -= LevelUp;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.CheckIfShouldFlip(xInput, yInput);
        Movement?.SetVelocity(new Vector2(xInput, yInput).normalized, playerSOData.moveSpeed);
        if (Movement.CurrentVelocity != Vector2.zero)
        {
            Movement?.SetLatestVelocity(Movement.CurrentVelocity);
           // Movement?.CheckCombatHitBoxDirection(xInput, yInput);
            partner.playerDirection = Movement.CurrentVelocity;
            partner.anim.SetFloat("moveY", partner.playerDirection.y);
            partner.anim.SetFloat("moveX", partner.playerDirection.x);
            partner.lastDirection = partner.playerDirection;
        }
        if (!isExitingState)
        {
            if(xInput == 0 && yInput == 0)
            {
                PSM.ChangePartnerState(partner.IdleState);
            }
        }
        if (primaryAttackInput)
        {
            PSM.ChangePartnerState(partner.PrimaryAttackState);
        }
        else if(xInput !=0 && yInput != 0 && primaryAttackInput)
        {
            PSM.ChangePartnerState(partner.PrimaryAttackState);
        }
        if (secondaryAttackInput)
        {
            PSM.ChangePartnerState(partner.SecondaryAttackState);

        }
        else if (xInput != 0 && yInput != 0 && secondaryAttackInput)
        {
            PSM.ChangePartnerState(partner.SecondaryAttackState);
        }
     
    }
 
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
