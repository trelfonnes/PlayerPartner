using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatsComponent : BossCoreComponent
{
    [SerializeField] BossStatsSO bossSOData;

    public void DecreaseHealth(float amount)
    { bossSOData.health -= amount;

        if(bossSOData.health <= 0)
        {
            //onhealth zero event triggered
            bossSOData.health = 0;
        }
        else if(bossSOData.health <= bossSOData.maxHealth * .34f)
        {
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
