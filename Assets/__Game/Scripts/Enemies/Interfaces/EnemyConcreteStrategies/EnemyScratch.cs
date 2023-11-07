using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScratch : IEnemyMelee
{
    public void Attack(EnemyWeapon weapon)
    {
        weapon.Enter();
    }
}
