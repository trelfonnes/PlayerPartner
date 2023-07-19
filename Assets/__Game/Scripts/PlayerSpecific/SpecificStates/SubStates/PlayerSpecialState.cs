using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialState : PlayerAbilityState
{
    private int xInput;
    private int yInput;
    public PlayerSpecialState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
