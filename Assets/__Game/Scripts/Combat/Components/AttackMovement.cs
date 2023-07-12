using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMovement : WeaponComponent<MovementData, AttackMovementData>
{
    Movement playerCoreMovement;
    Movement PlayerCoreMovement {
        get => playerCoreMovement ??= PlayerCore.GetCoreComponent<Movement>();

    }
  

private void HandleStartMovement()
    {
            PlayerCoreMovement.SetVelocity(currentAttackDataPlayer.Direction * player.lastDirection); //you can adjust the setveolicty in movment to
    
    }
    private void HandleStopMovement()
    {
            PlayerCoreMovement.SetVelocityZero();
       
    }
 

    protected override void Start()
    {
        base.Start();
            PlayerEventHandler.OnStartMovement += HandleStartMovement;
            PlayerEventHandler.OnStopMovement += HandleStopMovement;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
            PlayerEventHandler.OnStartMovement -= HandleStartMovement;
            PlayerEventHandler.OnStopMovement -= HandleStopMovement;
    }

}
