using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerKnockBack : WeaponComponent<KnockBackData, AttackKnockBack>
{
    PartnerActionHitBox hitBox;
    Movement movement;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IKnockBackable knockBackable))
            {
                if (!item.CompareTag("Partner") && !item.CompareTag("Player"))
                {


                    knockBackable.KnockBack(currentAttackDataPartner.Angle, currentAttackDataPartner.Strength, movement.facingCombatDirectionX, movement.facingCombatDirectionY);
                }
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        hitBox = GetComponent<PartnerActionHitBox>();
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
        movement = PartnerCore.GetCoreComponent<Movement>(); //safe to do this in start bc called in awake in inheritance
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;
    }
}
