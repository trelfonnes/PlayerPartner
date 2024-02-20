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

   public void HoldMovementForMelee()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    public void ContinueMovement(float timeToMove)
    {
        StartCoroutine(MoveAgain(timeToMove));
    }
    IEnumerator MoveAgain(float timeToMove)
    {
        yield return new WaitForSeconds(timeToMove);
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }
}
