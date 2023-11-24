using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetectedState : EnemyBasicState
{
    protected EnemyMovement EnemyMovement { get => enemyMovement ?? core.GetCoreComponent(ref enemyMovement); }
    private EnemyMovement enemyMovement;
    public EnemyPlayerDetectedState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, EnemyData data, string animBoolName) : base(enemy, ESM, enemySoData, data, animBoolName)
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
        Debug.Log(useMeleeAttack + "Melee bool");
        Debug.Log(useRangedAttack + "projectile bool");
        Debug.Log(inSightCircle + "sightcircle bool");

       if (useMeleeAttack)
        {

            ESM.ChangeState(enemy.MeleeState);
        }
      else if (useRangedAttack && !useMeleeAttack)
        {
            ESM.ChangeState(enemy.ProjectileState);
        }

        
        else
       {
       //     Debug.Log("going straight to think state");
           ESM.ChangeState(enemy.ThinkState);
        }
       

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
