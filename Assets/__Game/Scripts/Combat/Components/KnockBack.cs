using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : WeaponComponent<KnockBackData, AttackKnockBack>
{
    ActionHitBox hitbox;
    Movement movement;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if(item.TryGetComponent(out IKnockBackable knockBackable))
            {
                knockBackable.KnockBack(currentAttackDataPlayer.Angle, currentAttackDataPlayer.Strength, movement.facingCombatDirectionX, movement.facingCombatDirectionY);
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        hitbox = GetComponent<ActionHitBox>();
        hitbox.OnDetectedCollider2D += HandleDetectCollider2D;
        movement = PlayerCore.GetCoreComponent<Movement>();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitbox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }
}
