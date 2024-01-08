using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
public class BattleArenaManager : MonoBehaviour
{
    //public static event Action<bool> OnbattleEnd;

    [SerializeField] BattleArenaDataSO currentBattleArenaData;
    [SerializeField] BattleArena[] availableBattleArenas; //drag all arenas in the scene here.
    EnemySpawnManager enemySpawnManager = new EnemySpawnManager(); //make sure enemy object pool, and enemyPoolManager exist in the scene.
    ArenaPartnerManager partnerManager;
    public GameObject ForestCamera;
    private CinemachineVirtualCamera forestVC;

    public GameObject IceCamera;
    private CinemachineVirtualCamera iceVC;

    public GameObject LakeCamera;
    private CinemachineVirtualCamera lakeVC;

    public GameObject VolcanoCamera;
    private CinemachineVirtualCamera volcanoVC;

    public GameObject DesertCamera;
    private CinemachineVirtualCamera desertVC;


    private void OnEnable()
    {
        ArenaItemCollected.onEnemyDefeated += OpponentWasDefeated;
    }
    private void OnDisable()
    {
        ArenaItemCollected.onEnemyDefeated -= OpponentWasDefeated;

    }
    private void Awake()
    {
    }
    private void Start()
    {
        partnerManager = GetComponentInChildren<ArenaPartnerManager>();
        currentBattleArenaData = GameManager.Instance.currentNPCToBattle;
        BattleArena matchingArena = FindMatchingArena(currentBattleArenaData.areaType);
        ActivateProperCamera(currentBattleArenaData.areaType);
        if(matchingArena != null)
        {
            
            GameObject partner = partnerManager.ReturnPartnerType(currentBattleArenaData.partnerType);
            Debug.Log(partner + "Partner is");
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
    void ActivateProperCamera(AreaType arena)
    {
        if(arena == AreaType.Forest)
        {
            forestVC = ForestCamera.GetComponent<CinemachineVirtualCamera>();
            CameraSwitcher.SwitchArenaCamera(forestVC);
        }
        else if( arena == AreaType.IceCliff)
        {
            iceVC = IceCamera.GetComponent<CinemachineVirtualCamera>();
            CameraSwitcher.SwitchArenaCamera(iceVC);
        }
         else if( arena == AreaType.Lake)
        {
            lakeVC = LakeCamera.GetComponent<CinemachineVirtualCamera>();
            CameraSwitcher.SwitchArenaCamera(lakeVC);
        }
         else if( arena == AreaType.Desert)
        {
            desertVC = DesertCamera.GetComponent<CinemachineVirtualCamera>();
            CameraSwitcher.SwitchArenaCamera(desertVC);
        }
         else if( arena == AreaType.Volcano)
        {
            volcanoVC = VolcanoCamera.GetComponent<CinemachineVirtualCamera>();
            CameraSwitcher.SwitchArenaCamera(volcanoVC);
        }

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
