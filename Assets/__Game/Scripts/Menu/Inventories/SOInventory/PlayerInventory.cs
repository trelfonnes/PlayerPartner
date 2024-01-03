using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

 

   
}
