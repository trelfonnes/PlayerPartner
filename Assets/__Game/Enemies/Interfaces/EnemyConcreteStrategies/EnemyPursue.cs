using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursue : IEnemyMelee, IEnemyProjectile
{
    public void ShootProjectile(EnemyWeapon weapon, EnemyData data, Dictionary<int, WeaponDataSO> weaponDatas)
    {
        if (weaponDatas.ContainsKey(data.currentProjectileAttack))
        {
            WeaponDataSO attack = weaponDatas[data.currentProjectileAttack];
            weapon.SetNewEnemyData(attack);
            weapon.Enter();//what information does it need?

        }
        else
        {
            Debug.LogError("Enemysingle projectile current attack not found in WeaponDatas dictionary");
        }
    }
    public void Attack(EnemyWeapon weapon, EnemyData data, Dictionary<int, WeaponDataSO> weaponDatas)
    {
       
            if (weaponDatas.ContainsKey(data.currentMeleeAttack))
            {
                WeaponDataSO attack = weaponDatas[data.currentMeleeAttack];
                weapon.SetNewEnemyData(attack);
                weapon.Enter();//what information does it need?

            }
            else
            {
                Debug.LogError("Enemy Scratch: current attack not found in weaponDatas dictionary");
            }
        
    }

}
