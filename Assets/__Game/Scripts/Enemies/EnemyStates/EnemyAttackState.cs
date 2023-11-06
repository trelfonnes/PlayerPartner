using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBasicState
{
    bool attackUsed;
    
    public EnemyAttackState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName) : base(enemy, ESM, enemySoData, animBoolName)
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
        attackUsed = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (attackUsed)
        {
            if (isPlayerDetected || isPartnerDetected)
            {
                if (Time.time >= startTime + enemySoData.timeBetweenAttacks)
                {
                    attackUsed = false;//TODO may need a time between attacks float
                }
            }
            else if (inSightCircle)
            {
                ESM.ChangeState(enemy.MoveState);
            }
        }
        if (useMeleeAttack)
        {
            //set short range strategy
            attackUsed = true;
        }

        if (useRangedAttack && !useMeleeAttack)
        {
            //set ranged strategy
            attackUsed = true;
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
