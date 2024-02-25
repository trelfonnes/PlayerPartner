using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnockback : WeaponComponent<KnockBackData, AttackKnockBack>
{
    BossActionHitBox hitBox;
    BossMovement movement;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IKnockBackable knockBackable))
            {
                if (!item.CompareTag("Enemy") || !item.CompareTag("Boss"))
                {
                    knockBackable.KnockBack(currentAttackDataBoss.Angle, currentAttackDataBoss.Strength,0, -1); //always down for now.
                }


            }
        }
    }

    protected override void Start()
    {
        base.Start();
        hitBox = GetComponent<BossActionHitBox>();
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        movement = BossCompLoc.GetBossCoreComponent<BossMovement>();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }

}
