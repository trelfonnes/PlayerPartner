using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyData 
{
    EnemySOData readOnlyData;
    public EnemyData(EnemySOData readOnlyData)
    {
        this.readOnlyData = readOnlyData;
        health = readOnlyData.maxHealth;
        poise = readOnlyData.maxPoise;
    }
    public int currentProjectileAttack;
    public int currentMeleeAttack;

    public float health;
    public float poise;
    public bool lowHealth;
    public bool isStunned;
    public void ResetData()
    {
        health = readOnlyData.maxHealth;
        poise = readOnlyData.maxPoise;
        lowHealth = false;
        isStunned = false;
        currentProjectileAttack = 0;
        currentMeleeAttack = 0;
    }

}
