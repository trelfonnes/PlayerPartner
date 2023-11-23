using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBasicState
{
    protected EnemyMovement EnemyMovement { get => enemyMovement ?? core.GetCoreComponent(ref enemyMovement); }
    private EnemyMovement enemyMovement;
    protected EnemyCollisionSenses EnemyCollisionSenses { get => enemyCollisionSenses ?? core.GetCoreComponent(ref enemyCollisionSenses); }
    private EnemyCollisionSenses enemyCollisionSenses;
    IEnemyMove moveStrategy;

    public EnemyMoveState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, EnemyData data, string animBoolName, IEnemyMove moveStrategy) : base(enemy, ESM, enemySoData, data, animBoolName)
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
        moveStrategy.StartMovement(enemySoData.chargeSpeed, EnemyMovement, EnemyCollisionSenses); //passing in speed and the coreComponent

        //if (EnemyMovement.CurrentVelocity != Vector2.zero) This Is set inside the function called by the concrete strategy.
       // {
        //    enemy.anim.SetFloat("moveY", EnemyMovement.LastEnemyDirection.y);
         //   enemy.anim.SetFloat("moveX", EnemyMovement.LastEnemyDirection.x);
            
       // }
        
        if(isPlayerPartnerDetected)
        {

            ESM.ChangeState(enemy.PlayerDetectedState);
        }
        
        
       else if (!inSightCircle)
        {
            EnemyMovement.SetVelocityZero();

            ESM.ChangeState(enemy.ThinkState);
            
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
