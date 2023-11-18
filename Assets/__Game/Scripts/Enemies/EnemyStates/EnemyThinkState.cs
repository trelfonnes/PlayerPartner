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
        if (Time.time - startTime >= thinkTime * .5f)
        {
            if (enemySoData.lowHealth && inSightCircle)
            {
                Debug.Log("GOING TO LOW HEALTH");
                ESM.ChangeState(enemy.LowHealthState);
            }
           else if (inSightCircle)
            {
                ESM.ChangeState(enemy.MoveState);
            }
            

        }
        if (Time.time - startTime >= thinkTime)
        {
           

            ESM.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
   
}
