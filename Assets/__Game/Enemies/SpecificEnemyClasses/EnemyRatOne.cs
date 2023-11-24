using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRatOne : Enemy
{
    // VERY IMPORTANT that the weapon SO's are listed in order that I want. Cannot be random.
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
        lowHealthStrategy = new EnemyFlee();
        projectileStrategy = new EnemySingleProjectile();
        meleeStrategy = new EnemyScratch();
        itemSpawnStrategy = new EnemyItemRegular();
        
    }
}
