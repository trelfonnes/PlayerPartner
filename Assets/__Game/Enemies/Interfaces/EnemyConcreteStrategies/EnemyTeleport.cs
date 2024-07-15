using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : IEnemyMelee
{
    public void Attack(EnemyWeapon weapon, EnemyData data, Dictionary<int, WeaponDataSO> weaponDatas)
    {
        if (weaponDatas.ContainsKey(data.currentMeleeAttack))
        {
            WeaponDataSO attack = weaponDatas[data.currentMeleeAttack];
            weapon.SetNewEnemyData(attack);
            weapon.Enter();
        }
        else
        {
            Debug.LogError("Enemy Scratch: current attack not found in weaponDatas dictionary");
        }
    }


}
