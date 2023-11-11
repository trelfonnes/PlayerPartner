using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyWeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    SpriteRenderer enemyBaseSpriteRenderer;
    SpriteRenderer enemyWeaponsSpriteRenderer;
    EnemyMovement movement;

    int currentWeaponSpriteIndex;
    Sprite[] currentPhaseSprites;

    protected override void HandleEnemyEnter()
    {
        base.HandleEnemyEnter();
        currentWeaponSpriteIndex = 0;

    }

    void HandleEnterAttackPhase(AttackPhases phase)
    {
        currentWeaponSpriteIndex = 0;

        PhaseSprites[] filteredPhaseSprites = new PhaseSprites[currentAttackDataEnemy.PhaseSprites.Length];
        if (movement.LastEnemyDirection.x != 0)
        {
            filteredPhaseSprites = currentAttackDataEnemy.PhaseSprites
                     .Where(dataEnemy => dataEnemy.Phase == phase && dataEnemy.PhaseDirection == AttackPhases.EastFace)//checking to match the correct sprites with the partner.
                     .ToArray();
        }
        if (movement.LastEnemyDirection.x == 0 && movement.LastEnemyDirection.y > 0)
        {
            filteredPhaseSprites = currentAttackDataEnemy.PhaseSprites
                   .Where(dataEnemy => dataEnemy.Phase == phase && dataEnemy.PhaseDirection == AttackPhases.NorthFace)
                   .ToArray();
        }
        if (movement.LastEnemyDirection.x == 0 && movement.LastEnemyDirection.y < 0)
        {
            filteredPhaseSprites = currentAttackDataEnemy.PhaseSprites
                   .Where(dataEnemy => dataEnemy.Phase == phase && dataEnemy.PhaseDirection == AttackPhases.SouthFace)
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
        if (!isEnemyAttackActive)
        {
            enemyWeaponsSpriteRenderer.sprite = null;
            return;
        }
        if(currentWeaponSpriteIndex >= currentPhaseSprites.Length)
        {
            return;
        }
        enemyWeaponsSpriteRenderer.sprite = currentPhaseSprites[currentWeaponSpriteIndex];
        currentWeaponSpriteIndex++;
    }

    protected override void Start()
    {
        base.Start();
        enemyBaseSpriteRenderer = enemyWeapon.BaseGO.GetComponent<SpriteRenderer>();
        enemyWeaponsSpriteRenderer = enemyWeapon.WeaponSpriteGO.GetComponent<SpriteRenderer>();
        dataEnemy = enemyWeapon.weaponData.GetData<WeaponSpriteData>();
        enemyBaseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
        EnemyEventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
        movement = EnemyCore.GetCoreComponent<EnemyMovement>();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (enemyBaseSpriteRenderer)
            enemyBaseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        EnemyEventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
    }
}
