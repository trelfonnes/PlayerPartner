using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileAttackState : EnemyAttackState
{
    EnemyWeapon weapon;
    IEnemyProjectile projectileStrategy;
    public EnemyProjectileAttackState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName, EnemyWeapon weapon, IEnemyProjectile projectileStrategy) : base(enemy, ESM, enemySoData, animBoolName, weapon)
    {
        this.weapon = weapon;
        this.projectileStrategy = projectileStrategy;
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
        {//might need to do this from enter?? //call weapon.enter from concrete strategy
            projectileStrategy.ShootProjectile(weapon);//now it can communicate with weapon game object and pass any needed data.
            attackUsed = true;
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
