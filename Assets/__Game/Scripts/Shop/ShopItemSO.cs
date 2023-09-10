using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShopItemData", menuName = "ShopItem/Data")]

public class ShopItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public string confirmPurchaseText;
    public int itemPrice;
    public Sprite itemImage;
    public bool singlePurchaseItem;
    public int amountForSale;
    public GameObject itemPrefab;



}
