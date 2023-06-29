using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private int xInput;
    private int yInput;

    Weapon weapon;
    public PlayerAttackState(Player player, 
        PlayerStateMachine PSM, 
        PlayerSOData playerSOData, 
        PlayerData playerData, 
        string animBoolName, 
        Weapon weapon)
        : base(player, PSM, playerSOData, playerData, animBoolName)
    {
        this.weapon = weapon;
    }

    
    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void Enter()
    {
        base.Enter();
        weapon.Enter();
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
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

}
