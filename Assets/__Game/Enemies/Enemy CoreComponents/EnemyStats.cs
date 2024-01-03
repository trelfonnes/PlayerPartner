using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CoreComponent, IHealthChange, IPoiseDamageable
{
    [SerializeField] EnemySOData enemySOData;
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
        enemy.enemyData.health -= amount;
        if (enemy.enemyData.health <= 0)
        {
            statEvents.HealthZero();
            enemy.enemyData.health = 0;
            // statEvents.OnHealthZero();
            //TODO: Enemy will need separate tri receiver class
        }
        else if (enemy.enemyData.health <= enemySOData.maxHealth * 0.34f)
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
        if (enemy.enemyData.poise > 0)
        {
            enemy.enemyData.poise -= amount;

            if (enemy.enemyData.poise <= 0)
            {
                statEvents.PoiseZero();
                regeneratePoise = true;
                // statEvents.OnPoiseZero(); // This function invokes with SO event
            }
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
        stunnedTimer = Mathf.Clamp(stunnedTimer + (Time.deltaTime * poiseRecoveryRate), 0, enemySOData.poiseRefillTime);
        if(stunnedTimer >= enemySOData.poiseRefillTime)
        {
            enemy.enemyData.poise = enemySOData.maxPoise;
            if(enemy.enemyData.health > 0)
            {
                statEvents.PoiseRefilled();
            }
            statEvents.PoiseRefilled();
            enemy.enemyData.poise = enemySOData.maxPoise;
            stunnedTimer = 0;
            regeneratePoise = false;
        }
    }


}
