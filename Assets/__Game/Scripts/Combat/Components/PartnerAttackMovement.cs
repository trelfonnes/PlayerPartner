using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerAttackMovement : WeaponComponent
{
    Movement partnerCoreMovement;


    Movement PartnerCoreMovement
    {
        get => partnerCoreMovement ??= PartnerCore.GetCoreComponent<Movement>();
    }


    MovementData data;

    private void HandleStartMovement()
    {
        var partnerCurrentAttackData = data.AttackData[partnerWeapon.CurrentAttackCounter];
            PartnerCoreMovement.SetVelocity(partnerCurrentAttackData.Direction * partner.lastDirection);
    }

    private void HandleStopMovement()
    {
        PartnerCoreMovement.SetVelocityZero();
    }

    protected override void Awake()
    {
        base.Awake();
        data = partnerWeapon.Data.GetData<MovementData>();

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        PartnerEventHandler.OnStartMovement += HandleStartMovement;
        PartnerEventHandler.OnStopMovement += HandleStopMovement;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        PartnerEventHandler.OnStartMovement -= HandleStartMovement;
        PartnerEventHandler.OnStopMovement -= HandleStopMovement;
    }
}
