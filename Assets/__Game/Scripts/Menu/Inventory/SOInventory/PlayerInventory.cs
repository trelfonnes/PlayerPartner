using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/PlayerInventory")]
[System.Serializable]
public class PlayerInventory : ScriptableObject
{
    public List<inventoryItems> myInventory = new List<inventoryItems>();


    private static PlayerInventory instance;
    public static PlayerInventory Instance
    {
        get => instance;
        set => instance = value;
                


    }

    //dictionary to store key items in the player's inventory, NOT usable items e.g. potions

 private Dictionary<string, KeyItem> keyItems = new Dictionary<string, KeyItem>();

    public void AddKeyItem(KeyItem keyItem)
    {
        if (!keyItems.ContainsKey(keyItem.Name))
        {
            keyItems.Add(keyItem.Name, keyItem);
        }
        
    }

    public void RemoveKeyItem(KeyItem keyItem)
    {
        keyItems.Remove(keyItem.Name);
    }

    public bool HasKeyItem(string itemName)
    {
        return keyItems.ContainsKey(itemName);
    }

    public void ClearInventory()
    {
        keyItems.Clear();
    }

    // Can add any additional methods for managing the inventory

    private void OnEnable()
    {
        // TODO: when done with testing put conditio nto check if doesn't exist already
            keyItems = new Dictionary<string, KeyItem>();
        
    }

   
}
