using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBasicState
{
    float patrolTime;
    protected EnemyMovement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private EnemyMovement movement;
    private bool isPatrolTimeOver;

    public EnemyPatrolState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName) : base(enemy, ESM, enemySoData, animBoolName)
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
        isPatrolTimeOver = false;
        SetPatrolTime();
        Movement?.ChangeDirection(enemySoData.patrolSpeed);//will set a new random direction of 8 possible times patrol speed.
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.Patrol();
        if (Movement.CurrentVelocity != Vector2.zero)
        {
            enemy.anim.SetFloat("moveY", Movement.LastEnemyDirection.y);
            enemy.anim.SetFloat("moveX", Movement.LastEnemyDirection.x);

        }
        if (Time.time >= startTime + patrolTime)
        {
            isPatrolTimeOver = true;
        }

        if (isPlayerDetected || isPartnerDetected)
        {
            ESM.ChangeState(enemy.PlayerDetectedState); 
        }
        
       
        if (isTouchingWall)
        {
            Movement?.ChangeDirection(enemySoData.patrolSpeed);
        }
        else if (isPatrolTimeOver) //cycle between patrol and Idle
        {
            Movement.SetVelocityZero();
            ESM.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    void SetPatrolTime()
    {
        patrolTime = Random.Range(enemySoData.minPatrolTime, enemySoData.maxPatrolTime);
    }
}
