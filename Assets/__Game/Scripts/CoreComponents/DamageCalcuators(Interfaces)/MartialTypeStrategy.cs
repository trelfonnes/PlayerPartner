using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartialTypeStrategy : IAttackTypeDamageCalculation
{
    public float CalculateDamageModifier(float damageAmount, AttackType attackerType)
    {
        if (attackerType == AttackType.Air)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount;

        }  
        if (attackerType == AttackType.Martial)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount;

        }
        if (attackerType == AttackType.Ground)
        {
            damageAmount = (damageAmount / 2);
            return damageAmount;
        }
        if (attackerType == AttackType.Normal)
        {
            damageAmount = (damageAmount / 2);
            return damageAmount;
        } 
        if (attackerType == AttackType.Dragon)
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
