using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartnerWeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    SpriteRenderer partnerBaseSpriteRenderer;
    SpriteRenderer partnerWeaponsSpriteRenderer;

 
    int currentWeaponSpriteIndex;
    Sprite[] currentPhaseSprites;
    protected override void HandlePartnerEnter()
    {
        base.HandlePartnerEnter();
        currentWeaponSpriteIndex = 0;
    }

    private void HandleEnterAttackPhase(AttackPhases phase)
    {
        currentWeaponSpriteIndex = 0;
        currentPhaseSprites = currentAttackDataPartner.PhaseSprites.FirstOrDefault(dataPartner => dataPartner.Phase == phase).Sprites;

    }

        void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isPartnerAttackActive)
        {
            partnerWeaponsSpriteRenderer.sprite = null;
            return;
        }

            if(currentWeaponSpriteIndex >= currentPhaseSprites.Length)
        {
           // Debug.LogWarning($"{weapon.name} weapon Sprites length mismatch");
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, partner.lastDirection);
        //do something with last direction to see if sprite should flip on the x axis??
        partnerWeaponsSpriteRenderer.transform.rotation = targetRotation;

        partnerWeaponsSpriteRenderer.sprite = currentPhaseSprites[currentWeaponSpriteIndex];
        currentWeaponSpriteIndex++;
    }

    protected override void Start()
    {
        base.Start();
        
        partnerBaseSpriteRenderer = partnerWeapon.BaseGO.GetComponent<SpriteRenderer>();
        partnerWeaponsSpriteRenderer = partnerWeapon.WeaponSpriteGO.GetComponent <SpriteRenderer>();
        dataPartner = partnerWeapon.Data.GetData<WeaponSpriteData>();

        partnerBaseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
        PartnerEventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
    }
   
    protected override void OnDestroy()
    {
        base.OnDestroy();
        partnerBaseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        PartnerEventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;

    }


}
