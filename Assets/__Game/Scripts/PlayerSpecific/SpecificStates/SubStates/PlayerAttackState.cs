using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private int xInput;
    private int yInput;
    int inputIndex;
    Weapon weapon;
    public PlayerAttackState(Player player, 
        PlayerStateMachine PSM, 
        PlayerSOData playerSOData, 
        PlayerData playerData, 
        string animBoolName, 
        Weapon weapon,
        CombatInputs input)
        : base(player, PSM, playerSOData, playerData, animBoolName)
    {
        this.weapon = weapon;
        weapon.onExit += ExitHandler;  //DO I need to unsub??
        inputIndex = (int)input;
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
        weapon.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        weapon.CurrentInput = player.InputHandler.AttackInputs[inputIndex];
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
    void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}
