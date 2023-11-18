using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CoreComponent, IHealthChange, IPoiseDamageable
{
    [SerializeField] EnemySOData enemyData;
    //[SerializeField] EnemyStatEvents statEvents;
    //[SerializeField] EnemyHealthBar healthBar; // if implementing one.
    bool regeneratePoise = false;
    float poiseRecoveryRate = 2f;
    [SerializeField] public DefensiveType defensiveType;
    [SerializeField] EnemyStatEvents statEvents;
    float stunnedTimer = 0;

    void UpdateEHealthBar()
    {

    }
    public void DecreaseHealth(float amount)
    {

         enemyData.health -= amount;
        if (enemyData.health <= 0)
        {
            statEvents.HealthZero();
            enemyData.health = 0;
            // statEvents.OnHealthZero();
            //TODO: Enemy will need separate tri receiver class
        }
        else if (enemyData.health <= enemyData.maxHealth * 0.34f)
        {
            statEvents.HealthLow();
        }

        
    }
    public void IncreaseHealth(float amount)
    {

    }

    public void IncreaseMaxHealth(float amount)
    {

    }
    public void DamagePoise(float amount) 
    {
        enemyData.poise -= amount;
        if(enemyData.poise <= 0)
        {
            statEvents.PoiseZero();
            enemyData.poise = enemyData.maxPoise;
            regeneratePoise = true;
            // statEvents.OnPoiseZero(); // This function invokes with SO event
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (regeneratePoise)
        {
            StartStunnedTimer();
        }
    }
    void StartStunnedTimer()
    {
        stunnedTimer = Mathf.Clamp(stunnedTimer + (Time.deltaTime * poiseRecoveryRate), 0, enemyData.poiseRefillTime);
        if(stunnedTimer >= enemyData.poiseRefillTime)
        {
            statEvents.PoiseRefilled();
            enemyData.poise = enemyData.maxPoise;
            stunnedTimer = 0;
            regeneratePoise = false;
        }
    }


}
