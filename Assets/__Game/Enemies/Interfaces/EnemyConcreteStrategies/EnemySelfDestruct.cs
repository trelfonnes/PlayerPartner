using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelfDestruct : IEnemyLowHealth
{
    
   
    public void StartLowHealthStrategy(EnemySOData data, EnemyMovement movement)
    {
        Vector3 location = movement.transform.position;
        float size = 10f;
        float damage = data.maxHealth;
        AttackType attackType = data.offensiveType;
        float knockbackStrength = 4f;
        AreaAttackObject areaAttackInstance = AreaAttackObjectFactory.Instance.CreateAreaAttackObject(data.offensiveType);
        areaAttackInstance.PerformAreaAttack(location, size, damage, attackType, knockbackStrength);
    }
}
