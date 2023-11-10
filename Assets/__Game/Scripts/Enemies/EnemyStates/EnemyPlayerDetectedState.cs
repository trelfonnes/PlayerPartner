using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetectedState : EnemyBasicState
{
    protected EnemyMovement EnemyMovement { get => enemyMovement ?? core.GetCoreComponent(ref enemyMovement); }
    private EnemyMovement enemyMovement;
    public EnemyPlayerDetectedState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName) : base(enemy, ESM, enemySoData, animBoolName)
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
        EnemyMovement.SetVelocityZero();
        if (useMeleeAttack)
        {
            ESM.ChangeState(enemy.MeleeState);
        }
        if(useRangedAttack && !useMeleeAttack)
        {
            ESM.ChangeState(enemy.ProjectileState);
        }

        if (inSightCircle && !isPlayerDetected && !isPartnerDetected)
        {
            ESM.ChangeState(enemy.MoveState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
