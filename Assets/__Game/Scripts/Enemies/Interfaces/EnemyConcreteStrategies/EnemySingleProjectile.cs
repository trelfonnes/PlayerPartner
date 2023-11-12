using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySingleProjectile : IEnemyProjectile
{
   public void ShootProjectile(EnemyWeapon weapon, EnemySOData data, Dictionary<int, WeaponDataSO> weaponDatas)
    {
        //weapon.SetData(weaponData);
        //logic for shooting a single projectile
      
        if(weaponDatas.ContainsKey(data.currentAttack))
        {
            WeaponDataSO attack = weaponDatas[data.currentAttack];
            weapon.SetNewEnemyData(attack);
            weapon.Enter();//what information does it need?

        }
        else
        {
            Debug.LogError("Enemysingle projectile current attack not found in WeaponDatas dictionary");
        }

    }
}
