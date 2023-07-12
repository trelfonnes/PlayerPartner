using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerWeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    SpriteRenderer partnerBaseSpriteRenderer;
    SpriteRenderer partnerWeaponsSpriteRenderer;

 
    int currentWeaponSpriteIndex;
    protected override void HandlePartnerEnter()
    {
        base.HandlePartnerEnter();
        currentWeaponSpriteIndex = 0;
    }
        void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isPartnerAttackActive)
        {
            partnerWeaponsSpriteRenderer.sprite = null;
            return;
        }

        var currentAttackSprites = currentAttackDataPartner.Sprites;
            if(currentWeaponSpriteIndex >= currentAttackSprites.Length)
        {
           // Debug.LogWarning($"{weapon.name} weapon Sprites length mismatch");
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, partner.lastDirection);
        //do something with last direction to see if sprite should flip on the x axis??
        partnerWeaponsSpriteRenderer.transform.rotation = targetRotation;

        partnerWeaponsSpriteRenderer.sprite = currentAttackSprites[currentWeaponSpriteIndex];
        currentWeaponSpriteIndex++;
    }

    protected override void Start()
    {
        base.Start();
        
        partnerBaseSpriteRenderer = partnerWeapon.BaseGO.GetComponent<SpriteRenderer>();
        partnerWeaponsSpriteRenderer = partnerWeapon.WeaponSpriteGO.GetComponent <SpriteRenderer>();
        dataPartner = partnerWeapon.Data.GetData<WeaponSpriteData>();

        partnerBaseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);

    }
   
    protected override void OnDestroy()
    {
        base.OnDestroy();
        partnerBaseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);

    }


}
