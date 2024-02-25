using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
{
    BossActionHitBox hitBox;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IPoiseDamageable poiseDamageable))
            {
                if (!item.CompareTag("Enemy") || !item.CompareTag("Boss"))
                {
                    poiseDamageable.DamagePoise(currentAttackDataBoss.amount);
                }
            }

        }
    }
    protected override void Start()
    {
        base.Start();
        hitBox = GetComponent<BossActionHitBox>();
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }
}
