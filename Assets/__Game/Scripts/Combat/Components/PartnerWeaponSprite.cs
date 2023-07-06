using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerWeaponSprite : WeaponComponent
{
    SpriteRenderer partnerBaseSpriteRenderer;
    SpriteRenderer partnerWeaponsSpriteRenderer;

    WeaponSpriteData data;
 
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

        var currentAttackSprites = data.AttackData[partnerWeapon.CurrentAttackCounter].Sprites;
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

    protected override void Awake()
    {
        base.Awake();
        partnerBaseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
        partnerWeaponsSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();

        data = partnerWeapon.Data.GetData<WeaponSpriteData>();


        // TODO: fix this when creating weapon data

        //  partnerBaseSpriteRenderer = partnerWeapon.WeaponSpriteGO.GetComponent<SpriteRenderer>();
        // partnerWeaponsSpriteRenderer = partnerWeapon.WeaponSpriteGO.GetComponent <SpriteRenderer>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        partnerBaseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
        partnerWeapon.onEnter += HandlePartnerEnter;

    }
    protected override void OnDisable()
    {
        base.OnDisable();
        partnerBaseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        partnerWeapon.onEnter -= HandlePartnerEnter;

    }


}