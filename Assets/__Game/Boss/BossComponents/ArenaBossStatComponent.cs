using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBossStatComponent : BossCoreComponent, IHealthChange
{
    [SerializeField] BossStatsSO bossSOData;
    [SerializeField] EnemyStatEvents bossStatEvents;
    [SerializeField] BossHealthBarDisplay healthBarDisplay;
    float workingRestTime;
    void Start()
    {
        workingRestTime = Random.Range(1, bossSOData.restTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
