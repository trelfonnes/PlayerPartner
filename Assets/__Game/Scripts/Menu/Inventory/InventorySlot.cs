using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Attributes to Change")]
    [SerializeField] TextMeshProUGUI itemNumberText;
    [SerializeField] Image itemImage;

    [Header("Variables from the item")]
    public inventoryItems thisItem;
    public InventoryManager thisManager;

    public void Setup(inventoryItems newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

    public void ClickedOn()
    {
        if (thisItem)
        {
            thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem);
        }
    }

}
