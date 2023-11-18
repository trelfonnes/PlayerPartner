using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLowHealthState : EnemyBasicState
{
    float timeInState;
    IEnemyLowHealth lowHealthStrategy;
    protected EnemyMovement EnemyMovement { get => enemyMovement ?? core.GetCoreComponent(ref enemyMovement); }
    private EnemyMovement enemyMovement;
    public EnemyLowHealthState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName, IEnemyLowHealth lowHealthStrategy) : base(enemy, ESM, enemySoData, animBoolName)
    {
        this.lowHealthStrategy = lowHealthStrategy;
        timeInState = enemySoData.timeInLowHealth;
    }

    public override void Enter()
    {
        base.Enter();
        lowHealthStrategy.StartLowHealthStrategy(enemySoData.lowHealthSpeed, EnemyMovement);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isTouchingWall)
        {
            EnemyMovement.SetVelocityZero();
            EnemyMovement.ChangeDirection(enemySoData.lowHealthSpeed);
        }
        if (isPlayerPartnerDetected)
        {
            ESM.ChangeState(enemy.PlayerDetectedState);
        }
        else if(Time.time - startTime >= timeInState)
        {
            ESM.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
