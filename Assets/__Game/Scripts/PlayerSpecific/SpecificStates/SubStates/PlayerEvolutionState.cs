using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvolutionState : PlayerBasicState
{
    public PlayerEvolutionState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Movement?.SetVelocityZero();
        EvolveBehavior.OnEndEvolution += delegate (object sender, EvolveBehavior.OnEvolutionEventArgs e)
        {
            PSM.ChangeState(player.IdleState);
        };
        DevolveBehavior.OnDeEvolution += delegate (object sender, DevolveBehavior.OnDeEvolutionEventArgs e)
        {
            PSM.ChangeState(player.IdleState);
        };
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
