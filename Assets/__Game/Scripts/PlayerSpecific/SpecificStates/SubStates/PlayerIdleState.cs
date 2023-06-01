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
        Debug.Log("inIdle");

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
            if (switchInput)
            {
                PSM.ChangeState(player.WatchState);
            }
            if (interactInput && isTouchingCarryable)
            {
                PSM.ChangeState(player.HoldItemState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
