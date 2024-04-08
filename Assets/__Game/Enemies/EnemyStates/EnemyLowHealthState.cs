using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLowHealthState : EnemyBasicState
{
    float timeInState;
    IEnemyLowHealth lowHealthStrategy;
    protected EnemyMovement EnemyMovement { get => enemyMovement ?? core.GetCoreComponent(ref enemyMovement); }
    private EnemyMovement enemyMovement;
    protected EnemyStats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    EnemyStats stats;
    public EnemyLowHealthState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, EnemyData data, string animBoolName, IEnemyLowHealth lowHealthStrategy) : base(enemy, ESM, enemySoData, data, animBoolName)
    {
        this.lowHealthStrategy = lowHealthStrategy;
        timeInState = enemySoData.timeInLowHealth;
    }

    public override void Enter()
    {
        base.Enter();
        if (!enemySoData.selfDestructor || !enemySoData.rager)
        {
            lowHealthStrategy.StartLowHealthStrategy(enemySoData, EnemyMovement, CollisionSenses, Stats);
        }
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
        if (enemySoData.rager)
        {
            lowHealthStrategy.StartLowHealthStrategy(enemySoData, EnemyMovement, CollisionSenses, Stats);
        }
        if (isPlayerPartnerDetected && !enemySoData.rager)
        {
            ESM.ChangeState(enemy.PlayerDetectedState);
        }
        else if(Time.time - startTime >= timeInState)
        {
            if (enemySoData.selfDestructor)
            {
                lowHealthStrategy.StartLowHealthStrategy(enemySoData, EnemyMovement, CollisionSenses, Stats);
            }
            else
            {
                ESM.ChangeState(enemy.IdleState);

            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
