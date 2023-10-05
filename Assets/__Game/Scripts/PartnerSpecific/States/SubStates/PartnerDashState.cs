using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerDashState : PartnerAbilityState
{
    int amountOfDashesLeft;
    float timer;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    public PartnerDashState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
        amountOfDashesLeft = playerSOData.numberOfDashes;
    }

    public override void Enter()
    {
        base.Enter();
        amountOfDashesLeft--;
        timer = 0f;
        Movement?.SetVelocity(Movement.latestMovingVelocity * playerSOData.dashForce);

    }

    public override void Exit()
    {
        base.Exit();

        ResetAmountOfDashesLeft();
    }

         //   partner.AbilityCooldownTimer.Reset();
       // && partner.AbilityCooldownTimer.IsFinished()

    public bool CanDash()
    {
        if (amountOfDashesLeft > 0 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void DecreaseAmountOfDashesLeft()
    {
        amountOfDashesLeft--;
    }
    public void ResetAmountOfDashesLeft()
    {
        amountOfDashesLeft = playerSOData.numberOfDashes;
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocity(partner.playerDirection * playerSOData.dashForce);
        timer += Time.deltaTime;

        if (timer >= playerSOData.dashTime)
        {
            Movement?.SetVelocityZero();
            PSM.ChangePartnerState(partner.IdleState);
            partner.DashCooldownTimer.Reset();
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
