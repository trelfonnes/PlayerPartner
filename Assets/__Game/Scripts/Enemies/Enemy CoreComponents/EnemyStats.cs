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

    void UpdateEHealthBar()
    {

    }
    public void DecreaseHealth(float amount)
    {
        Debug.Log("Enemy Health " + enemyData.health);

         enemyData.health -= amount;
        Debug.Log("Enemy Health " + enemyData.health);

        if (enemyData.health <= 0)
        {
            enemyData.health = 0;
            // statEvents.OnHealthZero();
            //TODO: Enemy will need separate tri receiver class
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
        if(enemyData.poise <= 0)
        {
            enemyData.poise = 0;
            regeneratePoise = true;
            // statEvents.OnPoiseZero(); // This function invokes with SO event
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (regeneratePoise)
        {
            RegeneratePoise();
        }
    }
    void RegeneratePoise()
    {
        enemyData.poise = Mathf.Clamp(enemyData.poise + (Time.deltaTime * poiseRecoveryRate), 0, enemyData.maxPoise);
        if(enemyData.poise == enemyData.maxPoise)
        {
            regeneratePoise = false;
        }
    }


}
