using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHoldItemState : PlayerBasicState
{
    bool currentlyCarrying = false;
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
        if (Hits && !currentlyCarrying)
        {
            Debug.Log(Hits.transform.name);
            Hits.collider.GetComponent<ICarry>().Carry(carryPoint);
            currentlyCarrying = true;
        }
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
        // TODO condition for if item is thrown, go to Idle state

        if (!interactInput)
        {
            canExitState = true;
        }
        if (canExitState)
        {
            if (interactInput)
            {
                //TODO throw object
                PSM.ChangeState(player.IdleState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
