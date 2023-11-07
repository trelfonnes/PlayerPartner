using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRatOne : Enemy
{
 

    protected override void Awake()
    {
        SetStrategies();
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
    }
}
