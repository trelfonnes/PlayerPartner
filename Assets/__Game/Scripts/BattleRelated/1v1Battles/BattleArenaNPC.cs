using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArenaNPC : MonoBehaviour
{
    [SerializeField] BattleArenaDataSO thisNPCsData;
    

    //this class goes on the actual npc. TODO: functionality for dialogue and any other behaviors.

    public void CheckIfCanBattle()
    {
        if (!thisNPCsData.hasBeenDefeated && GameManager.Instance.CurrentGameState == GameState.overworld)
        {
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
