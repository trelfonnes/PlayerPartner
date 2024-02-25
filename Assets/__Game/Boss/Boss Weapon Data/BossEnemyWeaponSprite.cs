using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BossEnemyWeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    SpriteRenderer bossBaseSpriteRenderer;
    SpriteRenderer bossWeaponsSpriteRenderer;
    BossMovement movement;

    int currentWeaponSpriteIndex;
    Sprite[] currentPhaseSprites;

    protected override void HandleBossEnter()
    {
        base.HandleBossEnter();
        currentWeaponSpriteIndex = 0;

    }

    void HandleEnterAttackPhase(AttackPhases phase)
    {
        currentWeaponSpriteIndex = 0;

        PhaseSprites[] filteredPhaseSprites = new PhaseSprites[currentAttackDataBoss.PhaseSprites.Length];
       
            filteredPhaseSprites = currentAttackDataBoss.PhaseSprites
                     .Where(dataBoss => dataBoss.Phase == phase && dataBoss.PhaseDirection == AttackPhases.SouthFace)//checking to match the correct sprites with the partner.
                     .ToArray();
      


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
        if (!isBossAttackActive)
        {
            bossWeaponsSpriteRenderer.sprite = null;
            return;
        }
        if (currentWeaponSpriteIndex >= currentPhaseSprites.Length)
        {
            return;
        }
        bossWeaponsSpriteRenderer.sprite = currentPhaseSprites[currentWeaponSpriteIndex];
        currentWeaponSpriteIndex++;
    }

    protected override void Start()
    {
        base.Start();
        bossBaseSpriteRenderer = bossWeapon.BaseGO.GetComponent<SpriteRenderer>();
        bossWeaponsSpriteRenderer = bossWeapon.WeaponSpriteGO.GetComponent<SpriteRenderer>();
        dataBoss = bossWeapon.weaponData.GetData<WeaponSpriteData>();
        bossBaseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
        BossEventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
        movement = BossCompLoc.GetBossCoreComponent<BossMovement>();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (bossBaseSpriteRenderer)
            bossBaseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        BossEventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
    }
}
