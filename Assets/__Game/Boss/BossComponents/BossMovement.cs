using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : BossCoreComponent
{
    Rigidbody2D rb;


    protected override void Awake()
    {
        base.Awake();
        rb = GetComponentInParent<Rigidbody2D>();
        
    }

    public void MoveTowards(Vector2 targetPosition, float moveSpeed)
    {
        Vector2 direction = (targetPosition - rb.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

   
}
