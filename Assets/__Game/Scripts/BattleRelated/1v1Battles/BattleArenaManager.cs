using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BattleArenaManager : MonoBehaviour
{
    //public static event Action<bool> OnbattleEnd;

    [SerializeField] BattleArenaDataSO currentBattleArenaData;
    [SerializeField] BattleArena[] availableBattleArenas; //drag all arenas in the scene here.
    EnemySpawnManager enemySpawnManager = new EnemySpawnManager(); //make sure enemy object pool, and enemyPoolManager exist in the scene.

    private void OnEnable()
    {
        ArenaItemCollected.onEnemyDefeated += OpponentWasDefeated;
    }
    private void OnDisable()
    {
        ArenaItemCollected.onEnemyDefeated -= OpponentWasDefeated;

    }
    private void Start()
    {
        currentBattleArenaData = GameManager.Instance.currentNPCToBattle;
        BattleArena matchingArena = FindMatchingArena(currentBattleArenaData.areaType);

        if(matchingArena != null)
        {
            
            GameObject partner = PartnerManager.Instance.ReturnPartnerType(currentBattleArenaData.partnerType);
            partner.transform.position = matchingArena.partnerSpawnPoint.position;
            partner.SetActive(true); //make sure the referenced partner prefab in partner manager are the ones already in the scene innactive.

            for (int i = 0; i < Mathf.Min(matchingArena.enemySpawnPoints.Length, currentBattleArenaData.enemiesToSpawn); i++)
            {
                enemySpawnManager.SpawnEnemy(currentBattleArenaData.enemyType, matchingArena.enemySpawnPoints[i], currentBattleArenaData.areaType);

            }
        }
        else
        {
            Debug.LogError("Matching Battle Arena not found for AreaType: " + currentBattleArenaData.areaType);
        }
    }

  BattleArena FindMatchingArena(AreaType areaType)
    {
        foreach(var arena in availableBattleArenas)
        {
            if(arena.arenaAreaType == areaType)
            {
                return arena;
            }
        }
        return null;
    }
    public void OpponentWasDefeated() // call from event when the victory item is collected.
    {
        currentBattleArenaData.hasBeenDefeated = true;

    }
    public void PlayerWasDefeated()
    {
        currentBattleArenaData.hasBeenDefeated = false;


    }
}
