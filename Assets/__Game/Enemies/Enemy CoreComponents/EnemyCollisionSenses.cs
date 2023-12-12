using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionSenses : CollisionSenses
{
    public float CharacterCheckDistance { get => characterCheckDistance; set => characterCheckDistance = value; }
    public Transform playerTransform { get; private set; }
    public Transform partnerTransform { get; private set; }
    public Collider2D DetectedTriggerCollider { get; private set; }

    [SerializeField] protected float characterCheckDistance = 6f;
    [SerializeField] protected float characterCheckRadius = 6f;
    [SerializeField] float projAndOverallFOVDistance;
    [SerializeField] float meleeFOVDistance;
    [SerializeField] float fieldOfViewAngle;

  

    [SerializeField] protected LayerMask whatIsPartner;
    [SerializeField] protected LayerMask whatIsPlayer;

    public bool EnemyWallCheck
    {
        get
        {
            RaycastHit2D[] results = new RaycastHit2D[50];
            int hits = Physics2D.RaycastNonAlloc(collisionsCheckPoint.position, enemy.enemyDirection, results, collisionCheckDistance, whatIsWall);
            return hits > 0;
        }
    }

    public bool InDetectionCircle
    {
        get
        {
            int playerAndPartnerLayerMask = whatIsPartner | whatIsPlayer;

            Collider2D[] results = new Collider2D[10];
            int count = Physics2D.OverlapCircleNonAlloc(groundCheckPoint.position, characterCheckRadius, results, playerAndPartnerLayerMask);
            return count > 0;
        }
    }



private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Partner"))
        {
            partnerTransform = collision.transform;
            Debug.Log(partnerTransform);
        }
        if(collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
    }


    public bool IsPlayerInFieldOfView
    {
        get
        {
            int playerAndPartnerLayerMask = whatIsPartner | whatIsPlayer;
            Vector2 directionToPlayer = enemy.enemyDirection;

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
                            Debug.Log("Player is in long range rangeFOV");
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
                            Debug.Log(partnerResults[i].collider.name + "Partner is in long rangeFOV");
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





    public bool IsPlayerInCloseRangeFieldOfView
    {
        get
        {
            int playerAndPartnerLayerMask = whatIsPartner | whatIsPlayer;
            Vector2 directionToPlayer = enemy.enemyDirection;

            bool playerInFOV = false;
            bool partnerInFOV = false;

            if (playerTransform != null)
            {
                float angleToPlayer = Vector2.Angle(directionToPlayer, playerTransform.position - transform.position);

                if (angleToPlayer < fieldOfViewAngle * 0.5f)
                {
                    RaycastHit2D[] playerResults = new RaycastHit2D[10];
                    int playerHits = Physics2D.RaycastNonAlloc(transform.position, directionToPlayer, playerResults, meleeFOVDistance, whatIsPlayer);

                    for (int i = 0; i < playerHits; i++)
                    {
                        if (playerResults[i].collider.CompareTag("Player"))
                        {
                            Debug.Log(playerResults[i].collider.name +"Player is in close rangeFOV");
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
                    int partnerHits = Physics2D.RaycastNonAlloc(transform.position, directionToPlayer, partnerResults, meleeFOVDistance, whatIsPartner);

                    for (int i = 0; i < partnerHits; i++)
                    {
                        if (partnerResults[i].collider.CompareTag("Partner"))
                        {
                            Debug.Log(partnerResults[i].collider.name + "Partner is in close rangeFOV");
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


   
 

    private void OnDrawGizmosSelected()
    {
        // Draw the field of view in the scene view
        DrawFieldOfView();
    }

    private void DrawFieldOfView()
    {
        float halfFOV = fieldOfViewAngle * 0.5f;

        // Draw the field of view cone
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, -halfFOV) * enemy.enemyDirection * projAndOverallFOVDistance
            );
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, halfFOV) * enemy.enemyDirection * projAndOverallFOVDistance
            );
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 0, -halfFOV) * enemy.enemyDirection) * projAndOverallFOVDistance);
        Gizmos.DrawLine(transform.position, transform.position + (Quaternion.Euler(0, 0, halfFOV) * enemy.enemyDirection) * projAndOverallFOVDistance);

        // Draw the detection range circle
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, projAndOverallFOVDistance
            );
    }



}



