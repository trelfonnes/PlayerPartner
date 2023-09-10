using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ShopItem : MonoBehaviour
{
    [Header("Variables coming in from the Item")]
    public ShopItemSO itemData;
    public ShopBehaviorManager thisManager;
    [Header("UI Attributes that change")]
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemPriceText;

    public void SetupShopItem(ShopBehaviorManager newManager, ShopItemSO newItem)
    {
        itemData = newItem;
        thisManager = newManager;
        if (itemData)
        {
            // set this model's UI image to be the newItem's sprite
            itemImage.sprite = itemData.itemImage;
            //Set cost of item through newItem
            itemPriceText.text = "" + itemData.itemPrice;
        }
    }

    public void ClickedOn() //when the player clicks the image to buy
    {
        thisManager.SetupDescriptionAndButton(itemData.itemDescription, true, itemData);
        //itemData is used for the description, passed to controller and then set in text description box
    }


}
