using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicState : EnemyState
{
    protected bool isTouchingWall;
    protected bool isTouchingGround;
    protected bool canExitState;
    protected bool isPlayerDetected;
   // protected bool isPartnerDetected;
    protected bool useRangedAttack;
    protected bool useMeleeAttack;
    protected bool inSightCircle;
    protected EnemyCollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private EnemyCollisionSenses collisionSenses;


    public EnemyBasicState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName) : base(enemy, ESM, enemySoData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTouchingWall = CollisionSenses.EnemyWallCheck;
        isTouchingGround = CollisionSenses.GroundCheck;
        isPlayerDetected = CollisionSenses.PlayerCheck;
       // isPartnerDetected = CollisionSenses.PartnerCheck;
        useRangedAttack = CollisionSenses.RangedAttackCheck;
        useMeleeAttack = CollisionSenses.MeleeAttackCheck;
        inSightCircle = CollisionSenses.SightCircle;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
