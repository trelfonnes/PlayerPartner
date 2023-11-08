using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Basic Data")]

[System.Serializable]
public class EnemySOData : ScriptableObject
{

    public float health;
    public float patrolSpeed = 4f;
    public float chargeSpeed = 5f;
    public float lowHealthSpeed = 5f;
    public float poise;
    public float maxPoise = 5;

    public float maxIdleTime = 3f;
    public float minIdleTime = 1f;
    public float maxPatrolTime = 4f;
    public float minPatrolTime = 1f;
    public float timeBetweenAttacks = .5f;

    public int currentAttack = 1;

    private void OnEnable()
    { currentAttack = 1; }

}


