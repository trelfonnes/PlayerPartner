using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Basic Data")]

[System.Serializable]
public class EnemySOData : ScriptableObject
{

    public float health;
    public float maxHealth;
    public float patrolSpeed = 4f;
    public float chargeSpeed = 5f;
    public float lowHealthSpeed = 5f;
    public float poise = 3f;
    public float maxPoise = 5f;
    public float poiseRefillTime;
    public float intelligence = 3f;
    public float maxIdleTime = 3f;
    public float minIdleTime = 1f;
    public float maxPatrolTime = 4f;
    public float minPatrolTime = 1f;
    public float timeBetweenAttacks = .5f;
    public float timeInLowHealth = 2.5f;

    public int currentProjectileAttack;
    public int currentMeleeAttack;
    public bool lowHealth;
    IItemSpawnStrategy regularStrategy;
    IItemSpawnStrategy rareStrategy;
    IItemSpawnStrategy extraRareStrategy;

    //Set a reference to type of item spawn chance.
    public void ResetData()
    { 
        currentProjectileAttack = 0; 
        currentMeleeAttack = 0;
        health = maxHealth;
        poise = maxPoise;
        lowHealth = false;
    }

}


