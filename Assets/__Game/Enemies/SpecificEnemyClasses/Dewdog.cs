using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dewdog : Enemy
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
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();

    }
    protected override void SetStrategies()
    {
        base.SetStrategies();
        moveStrategy = new EnemyCharge();
        lowHealthStrategy = new EnemyFeint();
        projectileStrategy = new EnemySingleProjectile();
        meleeStrategy = new EnemyScratch();
        itemSpawnStrategy = new EnemyItemRegular();

    }
}
