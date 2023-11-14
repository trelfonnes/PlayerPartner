using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThinkState : EnemyBasicState
{
    float thinkTime;
    public EnemyThinkState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName) : base(enemy, ESM, enemySoData, animBoolName)
    {
        thinkTime = enemySoData.intelligence;
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
        if(Time.time - startTime >= thinkTime)
        {
            ESM.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
