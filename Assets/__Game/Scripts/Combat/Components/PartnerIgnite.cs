using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerIgnite : WeaponComponent<IgniteData, AttackIgnite>
{
    PartnerActionHitBox hitBox;
    

    void HandleDetectCollider2D(Collider2D[] colliders)
    {

        foreach (var item in colliders)
        {
            Debug.Log(item.name);
            if (item.TryGetComponent(out ILightable lightable))
            {
                Debug.Log("LIGHT");

                lightable.Light();
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
