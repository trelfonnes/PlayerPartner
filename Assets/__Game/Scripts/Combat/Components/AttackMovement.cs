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
