using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTypeStrategy : IAttackTypeDamageCalculation
{
    public int CalculateDamageModifier(int damageAmount, AttackType attackerType)
    {
        if (attackerType == AttackType.Electric)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount;

        }
        if (attackerType == AttackType.Ground)
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