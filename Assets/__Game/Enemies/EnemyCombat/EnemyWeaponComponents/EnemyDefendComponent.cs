using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefendComponent : WeaponComponent<DefendData, AttackDefend>
{
    EnemyTriReceiver combatReceiver;
    protected override void Start()
    {
        base.Start();
        combatReceiver = EnemyCore.GetCoreComponent<EnemyTriReceiver>();
        EnemyEventHandler.onStartDefend += StartDefend;
        EnemyEventHandler.onStopDefend += StopDefend;

    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        EnemyEventHandler.onStartDefend -= StartDefend;
        EnemyEventHandler.onStopDefend -= StopDefend;
    }
    void StartDefend()
    {
        combatReceiver.StartAndStopBlocking(true);
    }
    void StopDefend()
    {
        combatReceiver.StartAndStopBlocking(false);
    }
}
