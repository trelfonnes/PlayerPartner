using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScratch : IEnemyMelee
{
    public void Attack(EnemyWeapon weapon, WeaponDataSO weaponData)
    {
        weapon.SetData(weaponData); //Set data first, then enter.
        weapon.Enter();
    }
}
