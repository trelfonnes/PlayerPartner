using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTypeStrategy : IAttackTypeDamageCalculation
{
    public float CalculateDamageModifier(float damageAmount, AttackType attackerType)
    {
        if (attackerType == AttackType.Poison)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount;

        } 
        if (attackerType == AttackType.Fire)
        {
            damageAmount = (damageAmount * 2);
            return damageAmount;

        }
        if (attackerType == AttackType.Electric)
        {
            damageAmount = (damageAmount / 2);
            return damageAmount;
        } 
        if (attackerType == AttackType.Air)
        {
            damageAmount = (damageAmount / 2);
            return damageAmount;
        } 
        if (attackerType == AttackType.Martial)
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