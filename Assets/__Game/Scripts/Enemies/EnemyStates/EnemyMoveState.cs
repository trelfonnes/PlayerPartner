using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBasicState
{
    protected EnemyMovement EnemyMovement { get => enemyMovement ?? core.GetCoreComponent(ref enemyMovement); }
    private EnemyMovement enemyMovement;
    IEnemyMove moveStrategy;

    public EnemyMoveState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName, IEnemyMove moveStrategy) : base(enemy, ESM, enemySoData, animBoolName)
    {
        this.moveStrategy = moveStrategy;

    }
    // move state is entered only from attack state if long range and short range aggro
    // are no longer true. use collider to detect and move towards player/partner
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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
        if(isPartnerDetected || isPlayerDetected)
        {
            ESM.ChangeState(enemy.PlayerDetectedState);
        }
        else if (inSightCircle)
        {
            moveStrategy.StartMovement(enemySoData.chargeSpeed, EnemyMovement); //passing in speed and the coreComponent
        }
        if (!inSightCircle)
        {
            ESM.ChangeState(enemy.IdleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
