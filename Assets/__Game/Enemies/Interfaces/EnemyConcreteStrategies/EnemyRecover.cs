using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecover : IEnemyLowHealth
{
    public void StartLowHealthStrategy(EnemySOData data, EnemyMovement movement, EnemyCollisionSenses collisionSenses, EnemyStats stats, Particles particles)
    {
        //play healing sound w particle effec access stats or data to recover
        particles.StartParticles(ParticleType.Heal, collisionSenses.transform.position, collisionSenses.transform.rotation);
        float health = data.maxHealth;
        float percentToHeal = health *= .6f;
        float roundedHeal = Mathf.RoundToInt(percentToHeal);
        stats.IncreaseHealth(roundedHeal);
    }
}
