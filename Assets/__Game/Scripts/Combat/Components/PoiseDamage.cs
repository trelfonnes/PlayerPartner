using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
{
    ActionHitBox hitBox;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if(item.TryGetComponent(out IPoiseDamageable poiseDamageable))
            {
                if (!item.CompareTag("Partner") && !item.CompareTag("Player"))
                {
                    poiseDamageable.DamagePoise(currentAttackDataPlayer.amount);
                }
            }
        }

    }


    protected override void Start()
    {
        base.Start();
        hitBox = GetComponent<ActionHitBox>();
        if(hitBox)
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if(hitBox)
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }


}
