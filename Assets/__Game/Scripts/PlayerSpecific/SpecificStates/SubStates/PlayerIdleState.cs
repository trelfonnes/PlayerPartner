using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBasicState
{
    public PlayerIdleState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    } 

    public override void Enter()
    {
        base.Enter();
        canExitState = false;


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (xInput != 0 || yInput != 0)
        {
            PSM.ChangeState(player.MoveState);
        }

        if (!switchInput && !interactInput)
        {
            canExitState = true;
        }
        if (canExitState)
        {
            if (switchInput && !PlayerData.Instance.partnerIsDefeated)
            {
                player.evolutionEvents.SwitchToPartner();
                PSM.ChangeState(player.WatchState);

            }
            if (interactInput && isTouchingCarryable)
            {
                if (HitsToCarry)//&& !currentlyCarrying)
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
                        PSM.ChangeState(player.HoldItemState);
                    }
                }
            }
            if (interactInput && isTouchingInteractable)
            {
                if (HitsToInteract) 
                {
                  HitsToInteract.collider.GetComponent<IInteractable>().Interact();
                    canExitState = false;
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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

 
}
