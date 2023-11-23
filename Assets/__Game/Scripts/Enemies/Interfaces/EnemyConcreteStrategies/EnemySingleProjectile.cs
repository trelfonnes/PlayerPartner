using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySingleProjectile : IEnemyProjectile
{
   public void ShootProjectile(EnemyWeapon weapon, EnemyData data, Dictionary<int, WeaponDataSO> weaponDatas)
    {
        //weapon.SetData(weaponData);
        //logic for shooting a single projectile

        if(weaponDatas.ContainsKey(data.currentProjectileAttack))
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
}
