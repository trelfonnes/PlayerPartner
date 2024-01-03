using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
{
    EnemyActionHitBox hitBox;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach(var item in colliders)
        {
            if (item.TryGetComponent(out IPoiseDamageable poiseDamageable))
            {
                if (!item.CompareTag("Enemy"))
                {
                    poiseDamageable.DamagePoise(currentAttackDataEnemy.amount);
                }
            }

        }
    }
    protected override void Start()
    {
        base.Start();
        hitBox = GetComponent<EnemyActionHitBox>();
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }
}
