using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMovement : WeaponComponent
{
    Movement playerCoreMovement;
    Movement PlayerCoreMovement {
        get => playerCoreMovement ??= PlayerCore.GetCoreComponent<Movement>();

    }
  
    MovementData data;

private void HandleStartMovement()
    {
        var playerCurrentAttackData = data.AttackData[weapon.CurrentAttackCounter];
            PlayerCoreMovement.SetVelocity(playerCurrentAttackData.Direction * player.lastDirection); //you can adjust the setveolicty in movment to
    }
    private void HandleStopMovement()
    {
            PlayerCoreMovement.SetVelocityZero();
        Debug.Log("Movement SHould stop");
       
    }
    protected override void Awake()
    {
        base.Awake();
            data = weapon.Data.GetData<MovementData>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
            PlayerEventHandler.OnStartMovement += HandleStartMovement;
            PlayerEventHandler.OnStopMovement += HandleStopMovement;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
            PlayerEventHandler.OnStartMovement -= HandleStartMovement;
            PlayerEventHandler.OnStopMovement -= HandleStopMovement;
    }

}
