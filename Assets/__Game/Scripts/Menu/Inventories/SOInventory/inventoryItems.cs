using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class inventoryItems : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int numberHeld;
    public bool usable;
    public bool unique;
    public ItemType itemType;
    public bool statusHealingItem;
    public float amountToHeal;
    public void Use()
    {
        
        if (usable)
        {
            DecreaseAmount(1);
        }
    }
    public void DecreaseAmount(int amountToDecrease)
    {
        numberHeld -= amountToDecrease;
        if(numberHeld <= 0)
        {
            numberHeld = 0;
        }
    }
    public ItemType GetItemType()
    {
        return itemType;
    }
    public int ReturnAmount()
    {
        return numberHeld;
    }
}