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
    
    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentWeaponSpriteIndex = 0;
    }

    void HandleEnterAttackPhase(AttackPhases phase)
    {
        currentWeaponSpriteIndex = 0;
        currentPhaseSprites = currentAttackDataPlayer.PhaseSprites.FirstOrDefault(dataPlayer => dataPlayer.Phase == phase).Sprites;
    }

    void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isAttackActive)
        {
            weaponsSpriteRenderer.sprite = null;
            return;
        }
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

    }
 
    protected override void OnDestroy()
    {
        base.OnDestroy();
        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        PlayerEventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;

    }
}

