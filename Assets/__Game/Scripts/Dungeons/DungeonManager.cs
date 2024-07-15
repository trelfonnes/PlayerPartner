using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    //
    private Dictionary<string, List<string>> dungeonState = new Dictionary<string, List<string>>();

    private void Awake()
    {
        //load dungeon state dictionary
        if (ES3.KeyExists("dungeonStateDictionary"))
        {
        dungeonState = ES3.Load<Dictionary<string, List<string>>>("dungeonStateDictionary");
        }
    }

    public void CollectItem(string itemName) // called by items
    {
        if(!dungeonState.ContainsKey("collected_items") || !dungeonState["collected_items"].Contains(itemName))
        {
            UpdateDungeonState("collected_items", itemName); //create this method obviously. The state is what items have been collected, events have passed ect.
        }

        
    }
    public void CollectKey(string keyName) // called by keys
    {
        if (!dungeonState.ContainsKey("collected_keys") || !dungeonState["collected_keys"].Contains(keyName))
        {
            HandleBossKeyCollected("collected_keys", keyName);
        }
    }

    void HandleBossKeyCollected(string key, string value)
    {
        if (!dungeonState.ContainsKey(key))
        {
            dungeonState[key] = new List<string>();
        }

        if (!dungeonState[key].Contains(value))
        {
            // If the value does not exist, add it to the list
            dungeonState[key].Add(value);
        }
    }

    //Add methods here for tracking different keys such as boss or enemies in area defeated, ect.

    void UpdateDungeonState(string key, string value)
    {
        if (!dungeonState.ContainsKey(key))
        {
            dungeonState[key] = new List<string>();
        }
        if (!dungeonState[key].Contains(value))
        {
            // If the value does not exist, add it to the list
            dungeonState[key].Add(value);
            SaveDungeonProgress();
        }
    }

    public bool IsItemCollected(string itemName)
    {
        return IsKeyInDictionary("collected_items", itemName);
    }
    public bool IsKeyCollected(string keyName)
    {
        return IsKeyInDictionary("collected_keys", keyName);
    }

    bool IsKeyInDictionary(string key, string value)
    {
        if (dungeonState.ContainsKey(key))
        {
            return dungeonState[key].Contains(value);
        }
        return false;
    }

    public void SaveDungeonProgress()
    {
        ES3.Save<Dictionary<string, List<string>>>("dungeonStateDictionary", dungeonState);
    }
}
