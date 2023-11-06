using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLowHealthState : EnemyBasicState
{
    IEnemyLowHealth lowHealthStrategy;
    protected EnemyMovement EnemyMovement { get => enemyMovement ?? core.GetCoreComponent(ref enemyMovement); }
    private EnemyMovement enemyMovement;
    public EnemyLowHealthState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName, IEnemyLowHealth lowHealthStrategy) : base(enemy, ESM, enemySoData, animBoolName)
    {
        this.lowHealthStrategy = lowHealthStrategy;
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
        lowHealthStrategy.StartLowHealthStrategy(enemySoData.lowHealthSpeed, enemyMovement);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
