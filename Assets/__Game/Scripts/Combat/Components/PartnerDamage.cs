using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerDamage : WeaponComponent<DamageData, AttackDamage>
{
    PartnerActionHitBox hitBox;
   void HandleDetectCollider2D(Collider2D[] colliders)
    {

        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IDamageable damageable)) //using an output parameter instead of input
            {
                if (!item.CompareTag("Player") && !item.CompareTag("Partner"))
                {

                    //if something was found, can call the function from it
                    damageable.Damage(currentAttackDataPartner.Amount, currentAttackDataPartner.AttackType);
                }
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
