using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBasicState
{
    private Movement movement;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    public PlayerMoveState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.onFallStarted += StartFalling;
        canExitState = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.onFallStarted -= StartFalling;

    }
    public override void OnDisable()
    {
        base.OnDisable();
        player.onFallStarted -= StartFalling;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Movement?.CheckIfShouldFlip(xInput, yInput);
        Movement?.SetVelocity(new Vector2(xInput, yInput).normalized, playerSOData.moveSpeed);
        if (Movement.CurrentVelocity != Vector2.zero)
        {
          //  Movement?.CheckCombatHitBoxDirection(xInput, yInput);
            player.playerDirection = Movement.CurrentVelocity;
            player.anim.SetFloat("moveY", player.playerDirection.y);
            player.anim.SetFloat("moveX", player.playerDirection.x);
            player.lastDirection = player.playerDirection;
        }
        
        if (!isExitingState)
        {
            
            if (xInput == 0 && yInput == 0)
            {
                PSM.ChangeState(player.IdleState);
            }
        }
        if (!interactInput)
        {
            canExitState = true;
        }
        if (canExitState)
        {
            
            if (interactInput && isTouchingCarryable)
            {
                if (HitsToCarry )//&& !currentlyCarrying)
                {
                    if (HitsToCarry.collider.GetComponent<CarryableItem>().isHeavyCarryable && !playerSOData.carryHeavy)
                    {
                        Debug.Log("This is too heavy for you!");
                        return;
                    }
                    else
                    {
                        Debug.Log(HitsToCarry.transform.name);
                        HitsToCarry.collider.GetComponent<ICarry>().Carry(carryPoint, playerSOData.carryHeavy);
                        currentlyCarrying = true;
                        PSM.ChangeState(player.CarryItemState);
                    }
                }
                                 
            }
        }
        if (primaryAttackInput && !isWatching)
        {
            PSM.ChangeState(player.PrimaryAttackState);
        }
        if (secondaryAttackInput && !isWatching)
        {
            PSM.ChangeState(player.SecondaryAttackState);

        }

    }
    public void StartFalling()
    {
        PSM.ChangeState(player.FallingState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
