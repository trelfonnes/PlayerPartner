using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArenaManager : MonoBehaviour
{

    [SerializeField] BattleArenaDataSO battleArenaData;
    [SerializeField] BattleArena[] availableBattleArenas; //drag all arenas in the scene here.
    EnemySpawnManager enemySpawnManager = new EnemySpawnManager(); //make sure enemy object pool, and enemyPoolManager exist in the scene.
    private void Start()
    {
        if(battleArenaData == null)
        {
            Debug.LogError("BattleArenaManager is not assigned a BattleArenaDataSO");
            return;
        }
        BattleArena matchingArena = FindMatchingArena(battleArenaData.areaType);

        if(matchingArena != null)
        {
            
            GameObject partner = PartnerManager.Instance.ReturnPartnerType(battleArenaData.partnerType);
            partner.transform.position = matchingArena.partnerSpawnPoint.position;
            partner.SetActive(true); //make sure the referenced partner prefab in partner manager are the ones already in the scene innactive.

            for (int i = 0; i < Mathf.Min(matchingArena.enemySpawnPoints.Length, battleArenaData.enemiesToSpawn); i++)
            {
                enemySpawnManager.SpawnEnemy(battleArenaData.enemyType, matchingArena.enemySpawnPoints[i], battleArenaData.areaType);

            }
        }
        else
        {
            Debug.LogError("Matching Battle Arena not found for AreaType: " + battleArenaData.areaType);
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
}
