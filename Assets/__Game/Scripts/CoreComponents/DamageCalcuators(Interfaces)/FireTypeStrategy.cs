using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTypeStrategy : IAttackTypeDamageCalculation
{
    public int CalculateDamageModifier(int damageAmount, AttackType attackerType)
    {
        if(attackerType == AttackType.Water)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount; 
                      
        }
        if(attackerType == AttackType.Poison)
        {
            damageAmount = (damageAmount / 2);
            return damageAmount;
        }
        else
        {
            return damageAmount;
        }
    }
}
