using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTypeStrategy : IAttackTypeDamageCalculation
{
    public float CalculateDamageModifier(float damageAmount, AttackType attackerType)
    {
        return damageAmount;
        
    }
}
