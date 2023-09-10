using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New ShopItemInventory", menuName = "ShopItem/ShopInventory")]

public class ShopInventory : ScriptableObject
{
    public List<ShopItemSO> itemsInInventory;

}
