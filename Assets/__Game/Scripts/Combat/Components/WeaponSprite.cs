using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    // looks at base SR, references and listens for changes in sprite.
    SpriteRenderer baseSpriteRenderer;
    SpriteRenderer weaponsSpriteRenderer;
    int currentWeaponSpriteIndex;
    Sprite[] currentPhaseSprites;
    Movement movement;
    
    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentWeaponSpriteIndex = 0;
    }

  
    void HandleEnterAttackPhase(AttackPhases phase)
    {
        currentWeaponSpriteIndex = 0;
        PhaseSprites[] filteredPhaseSprites = new PhaseSprites[3]; //default
        if (movement.facingCombatDirectionX !=0)
        {
           filteredPhaseSprites = currentAttackDataPlayer.PhaseSprites
                    .Where(dataPlayer => dataPlayer.Phase == phase && dataPlayer.PhaseDirection == AttackPhases.EastFace)//checking to match the correct sprites with the player.
                    .ToArray();
        }
        if(movement.facingCombatDirectionX == 0 && movement.facingCombatDirectionY > 0)
        {
            filteredPhaseSprites = currentAttackDataPlayer.PhaseSprites
                   .Where(dataPlayer => dataPlayer.Phase == phase && dataPlayer.PhaseDirection == AttackPhases.NorthFace)
                   .ToArray();
        }
        if(movement.facingCombatDirectionX == 0 && movement.facingCombatDirectionY < 0)
        {
            filteredPhaseSprites = currentAttackDataPlayer.PhaseSprites
                   .Where(dataPlayer => dataPlayer.Phase == phase && dataPlayer.PhaseDirection == AttackPhases.SouthFace)
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
        if (!isAttackActive)
        {
            weaponsSpriteRenderer.sprite = null;
            return;
        }
        Debug.Log(currentWeaponSpriteIndex);
        Debug.Log(currentPhaseSprites.Length);
        if (currentWeaponSpriteIndex >= currentPhaseSprites.Length)
        {
            Debug.LogWarning($"{weapon.name} weapon Sprites length mismatch");
            return;
        }
        weaponsSpriteRenderer.sprite = currentPhaseSprites[currentWeaponSpriteIndex];
        currentWeaponSpriteIndex++;
    }
    protected override void Start()
    {
        base.Start();
     
          baseSpriteRenderer = weapon.BaseGO.GetComponent<SpriteRenderer>();
         weaponsSpriteRenderer = weapon.WeaponSpriteGO.GetComponent<SpriteRenderer>();
        Debug.Log(baseSpriteRenderer.gameObject.name);

        dataPlayer = weapon.Data.GetData<WeaponSpriteData>();
        baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
        PlayerEventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
        movement = PlayerCore.GetCoreComponent<Movement>();
    }
 
    protected override void OnDestroy()
    {
        base.OnDestroy();
        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        PlayerEventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;

    }
}

