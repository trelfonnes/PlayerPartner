using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetectedState : EnemyBasicState
{
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
        if(isPlayerDetected || isPartnerDetected)
        {
            ESM.ChangeState(enemy.AttackState);
        }
        else if (inSightCircle)
        {
            ESM.ChangeState(enemy.MoveState);
        }
    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
