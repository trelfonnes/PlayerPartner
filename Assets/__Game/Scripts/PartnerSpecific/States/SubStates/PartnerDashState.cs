using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerDashState : PartnerAbilityState
{
    int amountOfDashesLeft;
    float timer;

    public PartnerDashState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
        amountOfDashesLeft = playerSOData.numberOfDashes;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Dash state entered");
        amountOfDashesLeft--;
        timer = 0f;
        Movement?.SetVelocity(Movement.latestMovingVelocity * playerSOData.dashForce);

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Dash state exited");

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
        Debug.Log("In update for dash");

        Movement?.SetVelocity(Movement.latestMovingVelocity * playerSOData.dashForce);
        timer += Time.deltaTime;

        if (timer >= playerSOData.dashTime)
        {
            Movement?.SetVelocityZero();
            PSM.ChangePartnerState(partner.IdleState);
            partner.AbilityCooldownTimer.Reset();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
