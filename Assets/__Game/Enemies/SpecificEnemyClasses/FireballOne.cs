using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballOne : Enemy
{
    [SerializeField] private List<WeaponDataSO> meleeWeaponDatas;
    [SerializeField] private List<WeaponDataSO> projectileWeaponDatas;


    protected override void Awake()
    {
        SetStrategies();
        SetMeleeWeaponDatas(meleeWeaponDatas);
        SetProjectileWeaponDatas(projectileWeaponDatas);
        base.Awake();

    }
    protected override void SetStrategies()
    {
        base.SetStrategies();
        moveStrategy = new EnemyCharge();
        projectileStrategy = new EnemySingleProjectile();
        itemSpawnStrategy = new EnemyItemRegular();
        lowHealthStrategy = new EnemySelfDestruct();
        meleeStrategy = new EnemyFirePunch();
    }
   
}
