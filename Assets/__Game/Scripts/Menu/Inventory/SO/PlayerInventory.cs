using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{
    public List<inventoryItems> myInventory = new List<inventoryItems>();



    
}
