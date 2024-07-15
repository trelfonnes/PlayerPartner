using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class InventoryExchangeNPC : MonoBehaviour
{// attach this script to the dialogue manager
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] PlayerArtifactInventory artifactInventory;
  
    [SerializeField] GameObject itemToGive;//NPC will just spawn the item, not put direct in inventory

   

    bool HasRequestedItem(string requestedItem)
    {
        foreach (inventoryItems item in playerInventory.myInventory)
        {
            if(item.itemName == requestedItem)
            {
                return true;
            }
        } 
        return false;
    }

    void RemoveItemFromPlayerInventory(string itemToRemove)
    {
        foreach(inventoryItems item in playerInventory.myInventory)
        {
            if(item.itemName == itemToRemove)
            {
                Debug.Log(item.itemName + " was removed from inventory");
                item.DecreaseAmount(1);
                break;
            }
        }
    }
    bool HasRequestedArtifactItem(string requestedArtifact)
    {
        foreach (ArtifactInventoryItems item in artifactInventory.artifactInventory)
        {
            if (item.artifactName == requestedArtifact)
            {
                return true;
               
            }
        }
        return false;
    }
    void RemoveItemFromArtifactInventory(string artifactToRemove)
    {
        foreach(ArtifactInventoryItems item in artifactInventory.artifactInventory)
        {
            if(item.artifactName == artifactToRemove)
            {
                Debug.Log(item.artifactName + " was removed from artifact inventory");

                artifactInventory.artifactInventory.Remove(item);
                break;
            }
        }
    }
    private void OnEnable()
    {
        Lua.RegisterFunction("HasRequestedItem", this, SymbolExtensions.GetMethodInfo(() => HasRequestedItem(string.Empty)));
        Lua.RegisterFunction("HasRequestedArtifactItem", this, SymbolExtensions.GetMethodInfo(() => HasRequestedArtifactItem(string.Empty)));
        Lua.RegisterFunction("RemoveItemFromArtifactInventory", this, SymbolExtensions.GetMethodInfo(() => RemoveItemFromArtifactInventory(string.Empty)));
        Lua.RegisterFunction("RemoveItemFromPlayerInventory", this, SymbolExtensions.GetMethodInfo(() => RemoveItemFromPlayerInventory(string.Empty)));
       // Lua.RegisterFunction(nameof(AddOne), this, SymbolExtensions.GetMethodInfo(() => AddOne((double)0)));
    }
    private void OnDisable()
    {//if this is on the dialogue manager these are not need because it is persistant across scenes
       // Lua.UnregisterFunction(nameof(HasRequestedItem));
      //  Lua.UnregisterFunction(nameof(HasRequestedArtifactItem));
      //  Lua.UnregisterFunction(nameof(RemoveItemFromArtifactInventory));
     //   Lua.UnregisterFunction(nameof(RemoveItemFromPlayerInventory));
    }

}
