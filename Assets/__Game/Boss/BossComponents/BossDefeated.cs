using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefeated : BossCoreComponent
{
    [SerializeField] GameObject rewardForDefeat;

    public void Defeated()
    {
        CleanUp();
        SpawnReward();
    }

    void CleanUp()
    {

    }
    void SpawnReward()
    {

    }

}
