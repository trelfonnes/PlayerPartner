using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PixelCrushers.DialogueSystem; 

public class BossRewardItem : MonoBehaviour
{
    [SerializeField] EnemyStatEvents currentLevelBossStatEvents;
    [SerializeField] Transform spawnLocation;
    public static event Action onRewardCollected;
    IncrementOnDestroy incrementLuaVariableComp;
    protected  void Start()
    {
        currentLevelBossStatEvents.onBossEnemyDefeated += SpawnRewardItem;
        incrementLuaVariableComp = GetComponent<IncrementOnDestroy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner") && !collision.isTrigger)
        {
            SetVictoryResponse();
            onRewardCollected?.Invoke();
        }
    }
    void SetVictoryResponse()
    {
        int randomNumber = UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1;
        incrementLuaVariableComp.increment = randomNumber;
        // set Lua variable to randomNumber to increment, - for decrement
    }
    
    void SpawnRewardItem()
    {
        transform.position = spawnLocation.position;
        currentLevelBossStatEvents.onBossEnemyDefeated -= SpawnRewardItem;

    }
    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        currentLevelBossStatEvents.onBossEnemyDefeated -= SpawnRewardItem;

    }
}
