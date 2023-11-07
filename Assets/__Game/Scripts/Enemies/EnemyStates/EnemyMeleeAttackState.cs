using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    EnemyWeapon weapon;
    IEnemyMelee meleeStrategy;
    public EnemyMeleeAttackState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName, EnemyWeapon weapon, IEnemyMelee meleeStrategy) : base(enemy, ESM, enemySoData, animBoolName, weapon)
    {
        this.weapon = weapon;
        this.meleeStrategy = meleeStrategy;
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
        if (!attackUsed)
        {//might need to do this from enter??
            meleeStrategy.Attack(weapon);//now it can communicate with weapon game object and pass any needed data.
            attackUsed = true;
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
