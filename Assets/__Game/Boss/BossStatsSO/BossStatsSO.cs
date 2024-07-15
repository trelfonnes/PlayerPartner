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
    public float stamina;
    public float maxStamina;
    public float restTime;
    public float timeBetweenProjectiles;
    public float distancingLength;
    public float chargeBuffer;
    public ProjectileType projectileType;
    public bool isDefeated;
    public bool isLowHealth;
    private void OnEnable()
    {
        health = maxHealth;
        stamina = maxStamina;
        isDefeated = false;
        isLowHealth = false;
    }
}
