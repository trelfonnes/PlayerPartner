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
    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }
    public void Teleport(float minTeleportDistance, float maxTeleportDistance)
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        // Calculate a random distance between min and max teleport distances
        float randomDistance = Random.Range(minTeleportDistance, maxTeleportDistance);

        // Calculate the new position based on the random angle and distance
        Vector3 newPosition = rb.transform.position + new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * randomDistance;

        // Teleport the enemy to the new position instantly
        rb.transform.position = newPosition;
    }
}
