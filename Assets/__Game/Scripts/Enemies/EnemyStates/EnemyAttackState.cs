using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBasicState
{
    protected bool isAttackDone;
   
    public EnemyAttackState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName, EnemyWeapon weapon) : base(enemy, ESM, enemySoData, animBoolName)
    {
        
    }

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
        isAttackDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAttackDone)
        {
                ESM.ChangeState(enemy.ThinkState);
        }
       
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
