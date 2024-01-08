using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArenaNPC : MonoBehaviour, IInteractable
{
    [SerializeField] BattleArenaDataSO thisNPCsData;
    

    //this class goes on the actual npc. TODO: functionality for dialogue and any other behaviors.
    public void Interact()
    {
        //TODO refactor this out to worth with the dialogue system. Interact => dialogue => UI selection => CheckIfCanBattle();
        CheckIfCanBattle();
        Debug.Log("Interacting with NPC");
    }

    public void CheckIfCanBattle()
    {
        if (!thisNPCsData.hasBeenDefeated)
        {
            Debug.Log("Inside start battle logic");
            GameManager.Instance.currentNPCToBattle = thisNPCsData;
            OnStartTheArenaBattle();
        }
    }


    private void OnStartTheArenaBattle()
    {
        

        SceneLoaderUtility sceneLoad = new SceneLoaderUtility();
        sceneLoad.LoadScene("BattleArena");

    }



}
