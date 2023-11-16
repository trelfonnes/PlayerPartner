using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : WeaponComponent<KnockBackData, AttackKnockBack>
{
    EnemyActionHitBox hitBox;
    EnemyMovement movement;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IKnockBackable knockBackable))
            {
                if (!item.CompareTag("Enemy"))
                {
                    knockBackable.KnockBack(currentAttackDataEnemy.Angle, currentAttackDataEnemy.Strength, (int)movement.LastEnemyDirection.x, (int)movement.LastEnemyDirection.y);
                }
               
                
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        hitBox = GetComponent<EnemyActionHitBox>();
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        movement = EnemyCore.GetCoreComponent<EnemyMovement>();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }
}
