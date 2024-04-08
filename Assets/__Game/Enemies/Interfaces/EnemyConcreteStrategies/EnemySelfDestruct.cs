using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelfDestruct : IEnemyLowHealth
{
    
   
    public void StartLowHealthStrategy(EnemySOData data, EnemyMovement movement, EnemyCollisionSenses collisionSenses, EnemyStats stats)
    {
        Vector3 location = movement.transform.position;
        float size = 2.5f;
        float damage = data.maxHealth;
        AttackType attackType = data.offensiveType;
        float knockbackStrength = 4f;
        AreaAttackObject areaAttackInstance = AreaAttackObjectFactory.Instance.CreateAreaAttackObject(data.offensiveType);
        areaAttackInstance.PerformAreaAttack(location, size, damage, attackType, knockbackStrength);
    }
}
