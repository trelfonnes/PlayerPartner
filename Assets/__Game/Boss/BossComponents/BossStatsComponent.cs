using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatsComponent : BossCoreComponent, IHealthChange
{
    [SerializeField] BossStatsSO bossSOData;
    [SerializeField] EnemyStatEvents bossStatEvents;
    public void DecreaseHealth(float amount)
    { bossSOData.health -= amount;
        Debug.Log("Boss' Health: " + bossSOData.health);
        if(bossSOData.health <= 0)
        {
            //onhealth zero event triggered
            bossStatEvents.HealthZero();
            bossSOData.health = 0;
        }
        else if(bossSOData.health <= bossSOData.maxHealth * .34f)
        {
            bossStatEvents.HealthLow();

            //health low event trigger??
        }
    }
    public void IncreaseHealth(float amount)
    {
        bossSOData.health += amount;
        if(bossSOData.health >= bossSOData.maxHealth)
        {
            bossSOData.health = bossSOData.maxHealth;
        }
    }
    public void IncreaseMaxHealth(float amount)
    {
        bossSOData.maxHealth += amount;
    }

}
