using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnedState : EnemyBasicState
{
    protected EnemyMovement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private EnemyMovement movement;
    protected Particles Particles { get => particles ?? core.GetCoreComponent(ref particles); }
    private Particles particles;
    int particlesSpawned;
    public EnemyStunnedState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, EnemyData data, string animBoolName) : base(enemy, ESM, enemySoData, data, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        particlesSpawned = 0;
        statEvents.onHealthZero += SwitchToDefeated;
        enemy.enemyData.isStunned = true;


    }
    void SwitchToDefeated()
    {
        ESM.ChangeState(enemy.DefeatedState);
    }
    
    public override void Exit()
    {
        base.Exit();
        statEvents.onHealthZero -= SwitchToDefeated;
        enemy.enemyData.isStunned = false;


    }
    public override void OnDisable()
    {
        base.OnDisable();
        statEvents.onHealthZero -= SwitchToDefeated;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (particlesSpawned == 0)
        {
            Particles?.StartParticles(ParticleType.Stunned, enemy.transform.position, enemy.transform.rotation);
            particlesSpawned++;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
