using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerPoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
{
    PartnerActionHitBox hitBox;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IPoiseDamageable poiseDamageable))
            {
                poiseDamageable.DamagePoise(currentAttackDataPartner.amount);
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
