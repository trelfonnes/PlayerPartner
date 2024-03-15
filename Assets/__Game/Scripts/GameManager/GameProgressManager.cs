using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    
    private static readonly GameProgressManager instance = new GameProgressManager();

    private static Dictionary<string, ObjectActiveState> objectStateStorage = new Dictionary<string, ObjectActiveState>();

    static GameProgressManager()
    {
        // Additional initialization logic can be added here if needed
    }

    public static GameProgressManager Instance
    {
        get { return instance; }
    }

    public static ObjectActiveState ReturnObjectActiveState(string objectName)
    {
        if (objectStateStorage.ContainsKey(objectName))
        {
            return objectStateStorage[objectName];
        }
        else
        {
            return ObjectActiveState.Active;
        }
    }
    public static void StoreObjectActiveState(string objectName, ObjectActiveState state)
    {
        if (!objectStateStorage.ContainsKey(objectName))
        {
            objectStateStorage.Add(objectName, state);
        }
        else
        {
            objectStateStorage[objectName] = state;
        }
    }
    private void OnEnable()
    {
        SaveLoadManager.onGlobalLoad += LoadObjectStateStorage;
        SaveLoadManager.onGlobalSave += SaveObjectStateStorage;

    }
    private void OnDisable()
    {
        SaveLoadManager.onGlobalLoad -= LoadObjectStateStorage;
        SaveLoadManager.onGlobalSave -= SaveObjectStateStorage;
    }
    void LoadObjectStateStorage()
    {
        if (ES3.KeyExists("ObjectStateStorage"))
        {
            objectStateStorage = ES3.Load<Dictionary<string, ObjectActiveState>>("ObjectStateStorage");
        }
    }
    void SaveObjectStateStorage()
    {
        ES3.Save<Dictionary<string, ObjectActiveState>>("ObjectStateStorage", objectStateStorage);
    }

    //Example of calling this class from another:  
    // set the state of an object
    // GameProgressManager.SetObjectState("Artifact1", ObjectActiveState.Active);
    // Retrieve the state of an object
    //ObjectActiveState state = GameProgressManager.GetObjectState("Artifact1");

}

public enum ObjectActiveState
{
    Active,
    Innactive
}