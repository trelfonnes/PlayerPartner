using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NPCManager : MonoBehaviour
{
    public List<BattleArenaDataSO> NPCBattleArenaSOs;

    private void Awake()
    {
      
    }
    private void Start()
    {
        SaveLoadManager.Instance.InitializeNPCManager(this);
    }

    public List<BattleArenaDataSO> GetListOfNPCSOs()
    {
        return new List<BattleArenaDataSO>(NPCBattleArenaSOs); // I'm not convinced this will work but chat says it will. 
    }
    public void RestoreSavedList(List<BattleArenaDataSO> savedList)
    {
        NPCBattleArenaSOs = new List<BattleArenaDataSO>(savedList);
    }

}
