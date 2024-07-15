using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDataManager : MonoBehaviour
{
    [SerializeField] List<NPCDataSO> allNPCData;

    public static NPCDataManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }
        
    }

    public NPCDataSO GetNPCData(string npcID) // called from every NPC class to get their data
    {
        foreach (NPCDataSO npcData in allNPCData)
        {
            if(npcData.npcID == npcID)
            {
                return npcData;
            }
        }
        Debug.LogError("Trel message: No NPC data found for " + npcID);
        return null; //no data found
    }

    void SaveListData()
    {
        ES3.Save<List<NPCDataSO>>("allNPCData", allNPCData);
        Debug.Log("NPCDataManager data has been SAVED");
    }
    void LoadListData()
    {
        if (ES3.KeyExists("allNPCData"))
        {
            Debug.Log("NPCDataManager data has been LOADED");

            allNPCData = ES3.Load<List<NPCDataSO>>("allNPCData");
        }
    }
    private void OnEnable()
    {
        SaveLoadManager.onGlobalLoad += LoadListData;
        SaveLoadManager.onGlobalSave += SaveListData;
    }
    private void OnDisable()
    {
        SaveLoadManager.onGlobalLoad -= LoadListData;
        SaveLoadManager.onGlobalSave -= SaveListData;
    }
}
