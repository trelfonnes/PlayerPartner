using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTypeStrategy : IAttackTypeDamageCalculation
{
    public int CalculateDamageModifier(int damageAmount, AttackType attackerType)
    {
        if (attackerType == AttackType.Poison)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount;

        }
        if (attackerType == AttackType.Electric)
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