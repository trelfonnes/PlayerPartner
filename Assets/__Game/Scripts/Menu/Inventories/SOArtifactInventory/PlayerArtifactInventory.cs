using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Artifact Inventory", menuName = "ArtifactInventory/PlayerArtifactInventory")]

public class PlayerArtifactInventory : ScriptableObject
{
    public List<ArtifactInventoryItems> artifactInventory = new List<ArtifactInventoryItems>();

    private static PlayerArtifactInventory instance;
    public static PlayerArtifactInventory Instance
    {
        get => instance;
        set => instance = value;



    }

    public Dictionary<string, KeyItem> keyItems = new Dictionary<string, KeyItem>();

    public void AddKeyItem(KeyItem keyItem)
    {
        if (!keyItems.ContainsKey(keyItem.Name))
        {
            keyItems.Add(keyItem.Name, keyItem);
            artifactInventory.Add(keyItem.keyItemInventoryDescriptionSO);
            Debug.Log("Hitting in teh add key item via artifact inventory");
        }

    }

    public void RemoveKeyItem(KeyItem keyItem)
    {
        keyItems.Remove(keyItem.Name);
    }

    public bool HasKeyItem(string itemName)
    {
        Debug.Log(itemName);
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
