using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTypeStrategy : IAttackTypeDamageCalculation
{
    public int CalculateDamageModifier(int damageAmount, AttackType attackerType)
    {
        if (attackerType == AttackType.Air)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount;

        }
        if (attackerType == AttackType.Fire)
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
