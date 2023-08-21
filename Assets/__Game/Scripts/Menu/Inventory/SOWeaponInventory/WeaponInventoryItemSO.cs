using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newWeaponInentoryItemData", menuName = "Inventory/Weapon InventoryItem/Inventory Weapon Item Data", order = 0)]

public class WeaponInventoryItemSO : ScriptableObject
{
    [SerializeField] public SecondaryWeaponState secondaryType;
    [SerializeField] public PrimaryWeaponState primaryType;
    public string weaponDescription;
    public string weaponName;
    public Sprite weaponImage;
    public bool isPlayerWeapon;
    public bool isPartnerWeapon;
    public bool isPrimary;
    public bool isPartnerOne;
    public bool isPartnerTwo;
    public bool isPartnerThree;
}
    