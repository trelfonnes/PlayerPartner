using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTypeStrategy : IAttackTypeDamageCalculation
{
    public int CalculateDamageModifier(int damageAmount, AttackType attackerType)
    {
        
            return damageAmount;
        
    }
}
