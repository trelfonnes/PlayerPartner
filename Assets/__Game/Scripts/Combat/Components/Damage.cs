using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : WeaponComponent<DamageData, AttackDamage>
{
    ActionHitBox hitBox;
    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IDamageable damageable)) //using an output parameter instead of input
            {
                if (!item.CompareTag("Partner") || !item.CompareTag("Player"))
                {
                    //if something was found, can call the function from it
                    
                    damageable.Damage(currentAttackDataPlayer.Amount);
                }
            }
        }
    }
    protected override void Start()
    {
        base.Start();
        hitBox = GetComponent<ActionHitBox>();
        hitBox.OnDetectedCollider2D += HandleDetectCollider2D;

    }

   
    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnDetectedCollider2D -= HandleDetectCollider2D;

    }
}
