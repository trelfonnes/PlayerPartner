using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatsComponent : BossCoreComponent, IHealthChange
{
    [SerializeField] BossStatsSO bossSOData;
    [SerializeField] EnemyStatEvents bossStatEvents;
    [SerializeField] BossHealthBarDisplay healthBarDisplay;
    public void DecreaseHealth(float amount)
    { bossSOData.health -= amount;
        UpdateHealthBar(bossSOData.health, bossSOData.maxHealth);

        Debug.Log("Boss' Health: " + bossSOData.health);
        if(bossSOData.health <= 0)
        {
            //onhealth zero event triggered
            bossStatEvents.HealthZero();
            bossSOData.health = 0;
            UpdateHealthBar(bossSOData.health, bossSOData.maxHealth);
        }
        else if(bossSOData.health <= bossSOData.maxHealth * .34f)
        {
            bossStatEvents.HealthLow();

        }
    }
    public void IncreaseHealth(float amount)
    {
        bossSOData.health += amount;
        UpdateHealthBar(bossSOData.health, bossSOData.maxHealth);

        if (bossSOData.health >= bossSOData.maxHealth)
        {
            bossSOData.health = bossSOData.maxHealth;
            UpdateHealthBar(bossSOData.health, bossSOData.maxHealth);


        }
    }
    public void IncreaseMaxHealth(float amount)
    {
        bossSOData.maxHealth += amount;
        UpdateHealthBar(bossSOData.health, bossSOData.maxHealth);

    }
    void UpdateHealthBar(float health, float maxHealth)
    {
        healthBarDisplay.UpdateDisplay(health, maxHealth);
    }
}
