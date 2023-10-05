using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : WeaponComponent<CutData, AttackCut>
{

    ActionHitBox hitBox;

    void HandleDetectCollider2D(Collider2D[] colliders)
    {
        Debug.Log("cut");

        foreach (var item in colliders)
        {
            if(item.TryGetComponent(out ICutable cutable))
            {
                cutable.Cut();
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
