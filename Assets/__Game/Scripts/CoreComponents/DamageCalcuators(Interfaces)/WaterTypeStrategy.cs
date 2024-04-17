using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTypeStrategy : IAttackTypeDamageCalculation
{
    public float CalculateDamageModifier(float damageAmount, AttackType attackerType)
    {
        if (attackerType == AttackType.Air)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount;

        } 
        if (attackerType == AttackType.Electric)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount;

        }
        if (attackerType == AttackType.Fire)
        {
            damageAmount = (damageAmount / 2);
            return damageAmount;
        }  
        if (attackerType == AttackType.Water)
        {
            damageAmount = (damageAmount / 2);
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
