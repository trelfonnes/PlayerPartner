using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackTypeDamageCalculation
{
    int CalculateDamageModifier(int damageAmount, AttackType attackerType);
}
