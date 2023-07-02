using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponSprite : WeaponComponent
{
    // looks at base SR, references and listens for changes in sprite.
    SpriteRenderer baseSpriteRenderer;
    SpriteRenderer weaponsSpriteRenderer;

    

    [SerializeField] WeaponsSprites[] weaponSprites;
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
        var currentAttackSprites = weaponSprites[weapon.CurrentAttackCounter].Sprites;
        if (currentWeaponSpriteIndex >= currentAttackSprites.Length)
        {
            Debug.LogWarning($"{weapon.name} weapon Sprites length mismatch");
            return;
        }
        weaponsSpriteRenderer.sprite = currentAttackSprites[currentWeaponSpriteIndex];
        currentWeaponSpriteIndex++;
    }
    protected override void Awake()
    {
        base.Awake();
        baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
        weaponsSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();

        

        // TODO: fix this when creating weapon data
        //  baseSpriteRenderer = weapon.BaseGO.GetComponent<SpriteRenderer>();
        // weaponsSpriteRenderer = weapon.WeaponSpriteGO.GetComponent<SpriteRenderer>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
        weapon.onEnter += HandleEnter;

    }
    protected override void OnDisable()
    {
        base.OnDisable();
        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        weapon.onEnter -= HandleEnter;

    }
}
[Serializable]
public class WeaponsSprites
{
  [field: SerializeField]  public Sprite[] Sprites { get; private set; }
}
