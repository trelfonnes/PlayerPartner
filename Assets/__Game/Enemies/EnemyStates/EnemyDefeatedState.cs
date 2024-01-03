using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefeatedState : EnemyBasicState
{
    protected Particles Particles { get => particles ?? core.GetCoreComponent(ref particles); }
    private Particles particles;
    IEnemyItemSpawn itemSpawnStrategy;
    Transform itemSpawnPoint;
    int particlesSpawned = 0;
    public EnemyDefeatedState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, EnemyData data, string animBoolName, IEnemyItemSpawn itemSpawnStrategy, Transform itemSpawnPoint) : base(enemy, ESM, enemySoData, data, animBoolName)
    {
        this.itemSpawnStrategy = itemSpawnStrategy;
        this.itemSpawnPoint = itemSpawnPoint;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
     if (data.health <= 0)
      {
          Debug.Log("Try to spawn an item");
           itemSpawnStrategy.SpawnItem(itemSpawnPoint);
            if (particlesSpawned == 0)
            {
                Particles?.StartParticles(ParticleType.Defeated, enemy.transform.position, enemy.transform.rotation);
                particlesSpawned++;
            }
           
       }
        
        
    }

    public override void Exit()
    {
        base.Exit();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        particlesSpawned = 0;
       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
