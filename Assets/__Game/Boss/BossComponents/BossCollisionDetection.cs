using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionDetection : BossCoreComponent
{

    public Transform playerTransform { get; private set; }
    public Transform partnerTransform { get; private set; }

    [SerializeField] float fieldOfViewAngle;
    [SerializeField] float projAndOverallFOVDistance;

    [SerializeField] protected LayerMask whatIsPartner;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected LayerMask whatIsWall;
    [SerializeField] protected LayerMask whatIsMovePoint;

    [SerializeField] Transform collisionsCheckPoint;
    [SerializeField] float wallCheckRadius;
    private Vector2 wallCollisionDirection;

    [SerializeField] float movePointCheckRadius = .5f;
    public List<Transform> MovePoints;
    public Transform lastMovePoint;

    public bool WallCheck
    {
        get
        {
            Collider2D[] results = new Collider2D[10];
            int count = Physics2D.OverlapCircleNonAlloc(collisionsCheckPoint.position, wallCheckRadius, results, whatIsWall); 
            return count > 0;
        }
    }

    public Transform GetRandomMovePoint()
    {
        List<Transform> availableMovePoints = new List<Transform>(MovePoints);
        availableMovePoints.Remove(lastMovePoint);
        Transform randomMovePoint = availableMovePoints[Random.Range(0, availableMovePoints.Count)];
        lastMovePoint = randomMovePoint;
        return randomMovePoint;

    }

    public bool MovePointCheck
    {
        get
        {
            Collider2D[]
            results = new Collider2D[10];
            int count = Physics2D.OverlapCircleNonAlloc(collisionsCheckPoint.position, movePointCheckRadius, results, whatIsMovePoint);
            return count > 0;
        }
    }


    public bool IsPlayerInFieldOfView
    {
        get
        {
            int playerAndPartnerLayerMask = whatIsPartner | whatIsPlayer;
            Vector2 directionToPlayer = Vector2.down; //change this for dynamic direction of FOV if needed.

            bool playerInFOV = false;
            bool partnerInFOV = false;

            if (playerTransform != null)
            {
                float angleToPlayer = Vector2.Angle(directionToPlayer, playerTransform.position - transform.position);

                if (angleToPlayer < fieldOfViewAngle * 0.5f)
                {
                    RaycastHit2D[] playerResults = new RaycastHit2D[50];
                    int playerHits = Physics2D.RaycastNonAlloc(transform.position, directionToPlayer, playerResults, projAndOverallFOVDistance, whatIsPlayer);

                    for (int i = 0; i < playerHits; i++)
                    {
                        if (playerResults[i].collider.CompareTag("Player"))
                        {
                            playerInFOV = true;
                            break; // Exit the loop once player is found
                        }
                    }
                }
            }

            if (partnerTransform != null)
            {
                float angleToPartner = Vector2.Angle(directionToPlayer, partnerTransform.position - transform.position);

                if (angleToPartner < fieldOfViewAngle * 0.5f)
                {
                    RaycastHit2D[] partnerResults = new RaycastHit2D[10];
                    int partnerHits = Physics2D.RaycastNonAlloc(transform.position, directionToPlayer, partnerResults, projAndOverallFOVDistance, whatIsPartner);

                    for (int i = 0; i < partnerHits; i++)
                    {
                        if (partnerResults[i].collider.CompareTag("Partner"))
                        {
                            partnerInFOV = true;
                            break; // Exit the loop once partner is found
                        }
                    }
                }
            }

            // Return true if either player or partner is in FOV
            return playerInFOV || partnerInFOV;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
        if (collision.CompareTag("Partner"))
        {
            partnerTransform = collision.transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 averageNormal = CalculateAverageNormal(collision.contacts);
                DetermineCollisionSide(averageNormal);
        }
    }

    Vector2 CalculateAverageNormal(ContactPoint2D[] contacts)
    {
        Vector2 averageNormal = Vector2.zero;
        foreach (ContactPoint2D contactPoint in contacts)
        {
            averageNormal += contactPoint.normal;
        }
        averageNormal /= contacts.Length;
        return averageNormal;
    }
    private void DetermineCollisionSide(Vector2 normal)
    {
        // Example: Determine collision side based on normal vector
        if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
        {
            // More horizontally oriented collision
            if (normal.x > 0)
            {
                wallCollisionDirection = Vector2.left; // move to the left  Vector2.left;
            }
            else
            {
                wallCollisionDirection = Vector2.right;
               // MoveAwayFromWall(Vector2.right);
            }
        }
        else
        {
            // More vertically oriented collision
            if (normal.y > 0)
            {
                wallCollisionDirection = Vector2.down;
                //MoveAwayFromWall(Vector2.down);
            }
            else
            {
                wallCollisionDirection = Vector2.up;
                //MoveAwayFromWall(Vector2.up);
            }
        }
    }
    public Vector2 WallColNewDir() //call from node it will pass this to movement with a speed
    {
        return wallCollisionDirection;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        float halfFOV = fieldOfViewAngle * 0.5f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.forward);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.forward);
        Vector3 directionToPlayer = transform.TransformDirection(Vector3.down);

        Gizmos.DrawRay(transform.position, leftRayRotation * directionToPlayer * projAndOverallFOVDistance);
        Gizmos.DrawRay(transform.position, rightRayRotation * directionToPlayer * projAndOverallFOVDistance);
        Gizmos.DrawRay(transform.position, directionToPlayer * projAndOverallFOVDistance);
    }
}
