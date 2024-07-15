using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicState : EnemyState
{
    protected bool isTouchingWall;
    protected bool isTouchingGround;
    protected bool canExitState;
    protected bool isPlayerPartnerDetected;
    protected bool useRangedAttack;
    protected bool useMeleeAttack;
    protected bool inSightCircle;
    protected EnemyCollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private EnemyCollisionSenses collisionSenses;
    protected EnemyStatEvents statEvents;
    public EnemyBasicState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, EnemyData data, string animBoolName) : base(enemy, ESM, enemySoData, data, animBoolName)
    {
        statEvents = enemy.statEvents;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTouchingWall = CollisionSenses.EnemyWallCheck;
        isTouchingGround = CollisionSenses.GroundCheck;
        isPlayerPartnerDetected = CollisionSenses.IsPlayerInFieldOfView;
        useRangedAttack = CollisionSenses.IsPlayerInFieldOfView;
        useMeleeAttack = CollisionSenses.IsPlayerInCloseRangeFieldOfView;
        inSightCircle = CollisionSenses.InDetectionCircle;
    }

    public override void Enter()
    {
        base.Enter();
        statEvents.onHealthZero += EnemyDefeated;
        statEvents.onPoiseZero += PoiseZero;
        statEvents.onPoiseRefilled += PoiseRefilled;
        statEvents.onHealthLow += HealthLow;

    }

    public override void Exit()
    {
        base.Exit();
        statEvents.onHealthZero -= EnemyDefeated;
        statEvents.onPoiseZero -= PoiseZero;
        statEvents.onPoiseRefilled -= PoiseRefilled;
        statEvents.onHealthLow -= HealthLow;


    }
    public override void OnDisable()
    {
        base.OnDisable();
        statEvents.onHealthZero -= EnemyDefeated;
        statEvents.onPoiseZero -= PoiseZero;
        statEvents.onPoiseRefilled -= PoiseRefilled;
        statEvents.onHealthLow -= HealthLow;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    void EnemyDefeated()
    {
        if (data.health <= 0)
        {

            ESM.ChangeState(enemy.DefeatedState);
        }
    }
    void PoiseZero()
    {
        if (data.poise <= 0 && !data.isStunned && data.health > 0)
        {
            ESM.ChangeState(enemy.StunnedState);
        }
    }
    void PoiseRefilled()
    {
        if (data.isStunned)
        {
            ESM.ChangeState(enemy.PlayerDetectedState);
        }
    }
    void HealthLow()
    {
        if (data.health <= enemySoData.maxHealth * 0.34f)
        {
            data.lowHealth = true;
        }
    }
}
