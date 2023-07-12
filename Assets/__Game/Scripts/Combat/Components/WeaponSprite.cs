using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    // looks at base SR, references and listens for changes in sprite.
    SpriteRenderer baseSpriteRenderer;
    SpriteRenderer weaponsSpriteRenderer;



    int currentWeaponSpriteIndex;
    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentWeaponSpriteIndex = 0;
    }
    void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isAttackActive)
        {
            weaponsSpriteRenderer.sprite = null;
            return;
        }
        var currentAttackSprites = currentAttackDataPlayer.Sprites;
        if (currentWeaponSpriteIndex >= currentAttackSprites.Length)
        {
            Debug.LogWarning($"{weapon.name} weapon Sprites length mismatch");
            return;
        }
        weaponsSpriteRenderer.sprite = currentAttackSprites[currentWeaponSpriteIndex];
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

    }
 
    protected override void OnDestroy()
    {
        base.OnDestroy();
        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);

    }
}

