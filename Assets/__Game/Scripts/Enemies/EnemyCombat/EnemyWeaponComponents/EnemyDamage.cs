using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : WeaponComponent<DamageData, AttackDamage>
{
    EnemyActionHitBox hitBox;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IDamageable damageable))
            {
                if (!item.CompareTag("Enemy"))
                {
                    Debug.Log(item.transform.name);
                    Debug.Log("Damage from enemy" + currentAttackDataEnemy.Amount + currentAttackDataEnemy.AttackType);
                    damageable.Damage(currentAttackDataEnemy.Amount, currentAttackDataEnemy.AttackType);
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
