using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New BossDataSO", menuName = "BossDataSO")]

public class BossStatsSO : ScriptableObject
{
    public int health;
    public float moveSpeed;
    public float meleeTime;
    public float timeBetweenProjectiles;
    public ProjectileType projectileType;
}
