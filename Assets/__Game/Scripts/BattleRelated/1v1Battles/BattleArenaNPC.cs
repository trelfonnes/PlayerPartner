using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArenaNPC : MonoBehaviour, IInteractable
{
    [SerializeField] BattleArenaDataSO battleArenaData;
    [SerializeField] NPCDataSO nPCData;
    [SerializeField] string npcID;


    //this class goes on the actual npc. TODO: functionality for dialogue and any other behaviors.
    public void Interact()
    {
        //TODO refactor this out to worth with the dialogue system. Interact => dialogue => UI selection => CheckIfCanBattle();
        CheckIfCanBattle();
        Debug.Log("Interacting with arena NPC");
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

    private void Start()
    {
        //GetData on initialize
        NPCDataManager.Instance.GetNPCData(npcID);
    }

    private void OnStartTheArenaBattle()
    {
        

        SceneLoaderUtility sceneLoad = new SceneLoaderUtility();
        sceneLoad.LoadScene("BattleArena");

    }



}
