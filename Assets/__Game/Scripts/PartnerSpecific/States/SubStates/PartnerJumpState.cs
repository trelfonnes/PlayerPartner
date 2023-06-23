using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerJumpState : PartnerAbilityState
{
    int amountofJumpsLeft;
    Vector2 initialPosition;
    bool hasMovedJumpUnits = false;
    float jumpTimeout = 1.5f;
    float timer = 0f;

    public PartnerJumpState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
        amountofJumpsLeft = playerSOData.numberOfJumps;
    }

    public bool CanJump()
    {
        if(amountofJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void DecreaseAmountOfJumpsLeft()
    {
        amountofJumpsLeft--;
    }
    public void ResetAmountOfJumpsLeft()
    {
        amountofJumpsLeft = playerSOData.numberOfJumps;
        hasMovedJumpUnits = false;
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        initialPosition = partner.transform.position;
        DecreaseAmountOfJumpsLeft();
        Movement?.SetVelocityZero();
        CollisionSenses?.DisableHazardDetection();
        Movement?.SetVelocity(partner.playerDirection * playerSOData.jumpForce);
        timer = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        ResetAmountOfJumpsLeft();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float distance = Vector2.Distance(initialPosition, partner.transform.position);
        timer += Time.deltaTime;

        if(distance >= playerSOData.jumpDistance && !hasMovedJumpUnits)
        {
            Debug.Log("DIstance has been reached");
            hasMovedJumpUnits = true;
            Movement?.SetVelocityZero();
            CollisionSenses?.EnableHazardDetection();
            PSM.ChangePartnerState(partner.IdleState);
        }
        else if(timer >= jumpTimeout)
        {
            hasMovedJumpUnits = true;
            Movement?.SetVelocityZero();
            CollisionSenses?.EnableHazardDetection();
            PSM.ChangePartnerState(partner.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
