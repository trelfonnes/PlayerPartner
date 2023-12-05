using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTypeStrategy : IAttackTypeDamageCalculation
{
    public float CalculateDamageModifier(float damageAmount, AttackType attackerType)
    {
        if (attackerType == AttackType.Ground)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount;

        }
        if (attackerType == AttackType.Poison)
        {
            damageAmount = (damageAmount / 2);
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
