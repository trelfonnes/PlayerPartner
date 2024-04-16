using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlackboard  
{
    public Animator anim;
    public Vector2 TargetPosition { get; set; }
    public Vector2 moveDirection { get; set; }
    public bool isTouchingWall;
    public bool hasStarted;
    public float moveSpeed;
    public float timeBetweenProj;
    public float stamina;
    public float chargeBuffer;
    public bool chooseDirection;
    public bool isDefeated = false;
    public bool isLowHealth = false;
    public bool isFatigued = false;
    public float distancingLength = 4.5f;
    public float meleeTime;
    public float restTime;
    public ProjectileType projectileType;
    public Vector2 projectileDirection = Vector2.zero;

}
