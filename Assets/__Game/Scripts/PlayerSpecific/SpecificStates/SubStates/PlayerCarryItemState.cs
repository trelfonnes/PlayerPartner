using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryItemState : PlayerBasicState
{
    private Movement movement;
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    public PlayerCarryItemState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
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
        currentlyCarrying = true;
        
            
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.CheckIfShouldFlip(xInput, yInput);
        Movement?.SetVelocity(playerSOData.moveSpeed * (new Vector2(xInput, yInput).normalized));
        if (Movement?.CurrentVelocity != Vector2.zero)
        {
            player.playerDirection = Movement.CurrentVelocity;
            player.anim.SetFloat("moveY", player.playerDirection.y);
            player.anim.SetFloat("moveX", player.playerDirection.x);
        }

        if (!isExitingState)
        {

            if (Movement?.CurrentVelocity == Vector2.zero)
            {
                PSM.ChangeState(player.HoldItemState);
            }
        }
        if (!interactInput)
        {
            canExitState = true;
        }
        if (canExitState)
        {
            if (interactInput)
            {
                if (currentlyCarrying)
                {
                    HeldItemHit.collider.GetComponent<IThrow>().Throw(player.playerDirection); ;
                    currentlyCarrying = false;
                    PSM.ChangeState(player.MoveState);



                }
            }
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
