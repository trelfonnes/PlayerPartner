using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySingleProjectile : IEnemyProjectile
{
   public void ShootProjectile(EnemyWeapon weapon)
    {
        //logic for shooting a single projectile
        weapon.Enter();//what information does it need?
    }
}
