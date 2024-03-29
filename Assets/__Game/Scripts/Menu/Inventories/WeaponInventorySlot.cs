using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    [Header("UI Attributes to Change")]
    [SerializeField] Image weaponImage;

    [Header("Variables from this weapon")]
    public WeaponInventoryManager thisManager;
    public WeaponInventoryItemSO thisWeapon;

    public void Setup(WeaponInventoryItemSO newWeapon, WeaponInventoryManager newManager)
    {
        thisWeapon = newWeapon;
        thisManager = newManager;
        if (thisWeapon)
        {
           weaponImage.sprite = thisWeapon.weaponImage;
        }
    }

    public void ClickedOn()
    {
        if (thisWeapon)
        {
            thisManager.SetupDescriptionAndButton(thisWeapon.weaponDescription, true, thisWeapon);
        }
    }
}
