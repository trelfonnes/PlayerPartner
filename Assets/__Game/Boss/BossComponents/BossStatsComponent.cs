using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatsComponent : BossCoreComponent, IHealthChange
{
    [SerializeField] BossStatsSO bossSOData;
    [SerializeField] EnemyStatEvents bossStatEvents;
    [SerializeField] BossHealthBarDisplay healthBarDisplay;
    bool isFatigued;
    float workingRestTime;
    private void Start()
    {
        workingRestTime = Random.Range(1, bossSOData.restTime);
        if(healthBarDisplay == null)
        {
            healthBarDisplay = GetComponentInChildren<BossHealthBarDisplay>();
        }
    }
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

    public void RestoreStamina()
    {
        workingRestTime -= Time.deltaTime;
        if (workingRestTime <= 0)
        {
            workingRestTime = Random.Range(1, bossSOData.restTime);
            bossSOData.stamina = bossSOData.maxStamina;
            isFatigued = false;
            Movement.MoveOnOff(true);

        }
    }
    public void DecreaseStamina()
    {
        bossSOData.stamina -= Time.deltaTime;
        if(bossSOData.stamina <= 0)
        {
            bossStatEvents.StaminaZero();
            bossSOData.stamina = 0;
            EnterFatigued();
        }
    }
    public void EnterFatigued()
    {
        Movement.MoveOnOff(false);
       
        isFatigued = true;
    }
    public bool IsFatigued()
    {
        return isFatigued;
    }
}
