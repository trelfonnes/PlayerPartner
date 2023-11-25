using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAreaAttack 
{
    void PerformAreaAttack(Vector3 location, float colliderSize, float damage, AttackType attackType, float knockback);

}
