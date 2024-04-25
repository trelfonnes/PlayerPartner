using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArenaNPC : BasicNPC
{
    [SerializeField] BattleArenaDataSO battleArenaData;
    [SerializeField] NPCDataSO nPCData;
    [SerializeField] string npcID;


    //this class goes on the actual npc. TODO: functionality for dialogue and any other behaviors.
    public override void Interact()
    {
        base.Interact();
        //TODO refactor this out to worth with the dialogue system. Interact => dialogue => UI selection => CheckIfCanBattle();
        
       
    }

    public void CheckIfCanBattle()
    {
        if (!battleArenaData.hasBeenDefeated)
        {
            Debug.Log("Inside start battle logic");
            GameManager.Instance.currentNPCToBattle = battleArenaData;
            OnStartTheArenaBattle();
        }
    }

    protected override void Start()
    {
        base.Start();
        //GetData on initialize
        NPCDataManager.Instance.GetNPCData(npcID);
    }

    private void OnStartTheArenaBattle()
    {
        

        SceneLoaderUtility sceneLoad = new SceneLoaderUtility();
        sceneLoad.LoadScene("BattleArena");

    }



}
