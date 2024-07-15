using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : WeaponComponent<DamageData, AttackDamage>
{
    BossActionHitBox hitBox;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IDamageable damageable))
            {
                if (!item.CompareTag("Enemy") || !item.CompareTag("Boss"))
                {

                    damageable.Damage(currentAttackDataBoss.Amount, currentAttackDataBoss.AttackType);
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
