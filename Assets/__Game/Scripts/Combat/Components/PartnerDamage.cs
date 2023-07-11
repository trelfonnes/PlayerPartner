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
          if(item.TryGetComponent(out IDamageable damageable)) //using an output parameter instead of input
            {
                //if something was found, can call the function from it
                damageable.Damage(currentAttackDataPartner.Amount);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        hitBox = GetComponent<PartnerActionHitBox>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;

    }
}
