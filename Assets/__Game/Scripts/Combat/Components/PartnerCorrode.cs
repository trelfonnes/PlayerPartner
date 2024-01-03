using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerCorrode : WeaponComponent<CorrodeData, AttackCorrode>
{
    PartnerActionHitBox hitBox;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        Debug.Log("cut");

        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out ICorrode corrode))
            {
                corrode.Corrode();
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        hitBox = GetComponent<PartnerActionHitBox>();
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;

    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }


}
