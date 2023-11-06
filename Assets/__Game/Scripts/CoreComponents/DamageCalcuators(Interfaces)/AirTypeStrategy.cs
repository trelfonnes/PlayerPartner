using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTypeStrategy : IAttackTypeDamageCalculation
{
    public int CalculateDamageModifier(int damageAmount, AttackType attackerType)
    {
        //Weakness
        if (attackerType == AttackType.Fire)
        {
            damageAmount = damageAmount * 2;
            return damageAmount;
        }
    
        //Resistance
        if(attackerType == AttackType.Water)
        {
            damageAmount = damageAmount / 2;
            return damageAmount;
        }
        else
        {
            return damageAmount;
        }
    
    }


}
