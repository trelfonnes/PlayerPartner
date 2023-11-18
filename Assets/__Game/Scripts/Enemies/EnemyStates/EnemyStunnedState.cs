using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnedState : EnemyBasicState
{
    protected EnemyMovement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private EnemyMovement movement;
    public EnemyStunnedState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName) : base(enemy, ESM, enemySoData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
