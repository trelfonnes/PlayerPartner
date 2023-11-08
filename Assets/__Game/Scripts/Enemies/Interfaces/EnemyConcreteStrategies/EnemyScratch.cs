using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScratch : IEnemyMelee
{
    public void Attack(EnemyWeapon weapon, EnemySOData data, Dictionary<int, WeaponDataSO> weaponDatas)
    {
        if (weaponDatas.ContainsKey(data.currentAttack))
        {
            WeaponDataSO attack = weaponDatas[data.currentAttack];
            weapon.SetData(attack);
            weapon.Enter();//what information does it need?

        }
        else
        {
            Debug.LogError("Enemy Scratch: current attack not found in weaponDatas dictionary");
        }
    }
}
