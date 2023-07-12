using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerAttackMovement : WeaponComponent<MovementData, AttackMovementData>
{
    Movement partnerCoreMovement;


    Movement PartnerCoreMovement
    {
        get => partnerCoreMovement ??= PartnerCore.GetCoreComponent<Movement>();
    }

    private void HandleStartMovement()
    {
            PartnerCoreMovement.SetVelocity(currentAttackDataPartner.Direction * partner.lastDirection);
    }

    private void HandleStopMovement()
    {
        PartnerCoreMovement.SetVelocityZero();
    }

  
    protected override void Start()
    {
        base.Start();
        PartnerEventHandler.OnStartMovement += HandleStartMovement;
        PartnerEventHandler.OnStopMovement += HandleStopMovement;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        PartnerEventHandler.OnStartMovement -= HandleStartMovement;
        PartnerEventHandler.OnStopMovement -= HandleStopMovement;
    }
}
