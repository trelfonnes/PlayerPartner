using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New BossDataSO", menuName = "BossDataSO")]

public class BossStatsSO : ScriptableObject
{
    public float health;
    public float maxHealth;
    public float moveSpeed;
    public float meleeTime;
    public float timeBetweenProjectiles;
    public ProjectileType projectileType;
    public bool isDefeated;
    public bool isLowHealth;
    private void OnEnable()
    {
        health = maxHealth;
        isDefeated = false;
        isLowHealth = false;
    }
}
