using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldItemState : PlayerBasicState
{
    public PlayerHoldItemState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
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

        if (xInput != 0 || yInput != 0)
        {
            PSM.ChangeState(player.CarryItemState);
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

                    HeldItemHit.collider.GetComponent<IThrow>().SetDown(player.playerDirection);
                    currentlyCarrying = false;
                    PSM.ChangeState(player.IdleState);

                }
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
