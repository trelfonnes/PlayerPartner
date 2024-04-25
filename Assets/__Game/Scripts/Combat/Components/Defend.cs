using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : WeaponComponent<DefendData, AttackDefend>
{
   TriReceiver combatReceiver;

    protected override void Start()
    {
        base.Start();
        combatReceiver = PlayerCore.GetCoreComponent<TriReceiver>();
        PlayerEventHandler.onStartDefend += StartDefend;
        PlayerEventHandler.onStopDefend += StopDefend;
    }
    void StartDefend()
    {
        combatReceiver.StartAndStopBlocking(true);
    }
    void StopDefend()
    {
        combatReceiver.StartAndStopBlocking(false);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        PlayerEventHandler.onStartDefend -= StartDefend;
        PlayerEventHandler.onStopDefend -= StopDefend;
    }

}
