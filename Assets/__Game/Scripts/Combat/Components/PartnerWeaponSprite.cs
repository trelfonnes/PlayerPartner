using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartnerWeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    SpriteRenderer partnerBaseSpriteRenderer;
    SpriteRenderer partnerWeaponsSpriteRenderer;
    Movement movement;
 
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
        //  currentPhaseSprites = currentAttackDataPartner.PhaseSprites.FirstOrDefault(dataPartner => dataPartner.Phase == phase).Sprites;
        PhaseSprites[] filteredPhaseSprites = new PhaseSprites[currentAttackDataPartner.PhaseSprites.Length]; //default
        if (movement.facingCombatDirectionX != 0)
        {
            filteredPhaseSprites = currentAttackDataPartner.PhaseSprites
                     .Where(dataPartner => dataPartner.Phase == phase && dataPartner.PhaseDirection == AttackPhases.EastFace)//checking to match the correct sprites with the partner.
                     .ToArray();
        }
        if (movement.facingCombatDirectionX == 0 && movement.facingCombatDirectionY > 0)
        {
            filteredPhaseSprites = currentAttackDataPartner.PhaseSprites
                   .Where(dataPartner => dataPartner.Phase == phase && dataPartner.PhaseDirection == AttackPhases.NorthFace)
                   .ToArray();
        }
        if (movement.facingCombatDirectionX == 0 && movement.facingCombatDirectionY < 0)
        {
            filteredPhaseSprites = currentAttackDataPartner.PhaseSprites
                   .Where(dataPartner => dataPartner.Phase == phase && dataPartner.PhaseDirection == AttackPhases.SouthFace)
                   .ToArray();
        }


        if (filteredPhaseSprites.Length > 0)
        {
            currentPhaseSprites = filteredPhaseSprites[0].Sprites;
        }
        else
        {
            // Handle the case when no matching PhaseSprites array is found
            // You can set a default value or take appropriate action here
        }
    }

        void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isPartnerAttackActive)
        {
            partnerWeaponsSpriteRenderer.sprite = null;
            return;
        }
        Debug.Log(currentWeaponSpriteIndex);
        Debug.Log(currentPhaseSprites.Length);

        if (currentWeaponSpriteIndex >= currentPhaseSprites.Length)
        {
           // Debug.LogWarning($"{weapon.name} weapon Sprites length mismatch");
            return;
        }

      //  Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, partner.lastDirection);
        //do something with last direction to see if sprite should flip on the x axis??
       // partnerWeaponsSpriteRenderer.transform.rotation = targetRotation;

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
        movement = PartnerCore.GetCoreComponent<Movement>();
    }
   
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if(partnerBaseSpriteRenderer)
        partnerBaseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        PartnerEventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;

    }


}
