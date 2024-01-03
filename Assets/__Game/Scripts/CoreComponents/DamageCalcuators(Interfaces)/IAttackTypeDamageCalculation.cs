using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackTypeDamageCalculation
{
    float CalculateDamageModifier(float damageAmount, AttackType attackerType);
}
