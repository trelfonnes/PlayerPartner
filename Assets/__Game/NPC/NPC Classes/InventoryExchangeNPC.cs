using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryExchangeNPC : BasicNPC
{
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] PlayerArtifactInventory artifactInventory;
    [SerializeField] string itemToRemove;
    [SerializeField] string requestedItem;
    [SerializeField] string requestedArtifact;
    [SerializeField] string artifactToRemove;
    [SerializeField] GameObject itemToGive;//NPC will just spawn the item, not put direct in inventory

    public override void Interact()
    {
        base.Interact();
        if (npcData.askForRegularItem)
        {
            if (HasRequestedItem())
            {
                RemoveItemFromPlayerInventory();
            }
            else
            {
                //give a response that they don't have the item
                Debug.Log("You don't have the item");

            }
        }
        if (npcData.askForArtifactItem)
        {
            if (HasRequestedArtifactItem())
            {
                RemoveItemFromArtifactInventory();
            }
            else
            {
                //tell the player they don't have the artifact yet.
                Debug.Log("You don't have the artifact");
            }
        }
    }

    bool HasRequestedItem()
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

    void RemoveItemFromPlayerInventory()
    {
        foreach(inventoryItems item in playerInventory.myInventory)
        {
            if(item.itemName == itemToRemove)
            {
                Debug.Log(item.itemName + " was removed from inventory");
                playerInventory.myInventory.Remove(item);
                break;
            }
        }
    }
    bool HasRequestedArtifactItem()
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
    void RemoveItemFromArtifactInventory()
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


}
