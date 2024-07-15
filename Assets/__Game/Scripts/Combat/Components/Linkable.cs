using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linkable : WeaponComponent<LinkableData, AttackLinkable>
{
    ActionHitBox hitBox;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        foreach(var item in colliders)
        {
            if(item.TryGetComponent(out ILinkable linkable))
            {

                linkable.Link(currentAttackDataPlayer.Amount);
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
