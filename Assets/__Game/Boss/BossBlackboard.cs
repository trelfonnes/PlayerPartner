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
    public bool chooseDirection;
    public float meleeTime;
    public ProjectileType projectileType;
    public Vector2 projectileDirection = Vector2.zero;

}
