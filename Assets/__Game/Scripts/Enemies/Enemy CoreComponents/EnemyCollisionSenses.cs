using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionSenses : CollisionSenses
{ 
    public float CharacterCheckDistance { get => characterCheckDistance; set => characterCheckDistance = value; }
    public Transform playerTransform { get; private set; }
    public Transform partnerTransform { get; private set; }
    public Transform ChargeTarget { get; private set; }
   public Collider2D DetectedTriggerCollider { get; private set; }

    [SerializeField] protected float characterCheckDistance = 6f;
    [SerializeField] protected float RangeAttkCheckDistance = 4f;
    [SerializeField] protected float MeleeAttkCheckDistance = 1f;
    [SerializeField] protected float CharacterCheckRadius = 8f;
   [SerializeField] float projAndOverallFOVDistance;
   [SerializeField] float meleeFOVDistance;
   [SerializeField] float fieldOfViewAngle;

    public bool inMeleeFOV { get; private set; }
    public bool inProjectileFOV { get; private set; }
  
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
   

   
    public bool SightCircle
    {
        get
        {
            int playerAndPartnerLayerMask = whatIsPartner | whatIsPlayer;

            Collider2D[]
            results = new Collider2D[50];
            int count = Physics2D.OverlapCircleNonAlloc(groundCheckPoint.position, CharacterCheckRadius, results, playerAndPartnerLayerMask);
            return count > 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner"))
        {
            partnerTransform = collision.transform;
            ChargeTarget = collision.transform;

         inMeleeFOV = IsPlayerInFieldOfView(partnerTransform, fieldOfViewAngle, meleeFOVDistance);
         inProjectileFOV = IsPlayerInFieldOfView(partnerTransform, fieldOfViewAngle, projAndOverallFOVDistance);
        }
        else if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
            inMeleeFOV = IsPlayerInFieldOfView(playerTransform, fieldOfViewAngle, meleeFOVDistance);
            inProjectileFOV = IsPlayerInFieldOfView(playerTransform, fieldOfViewAngle, projAndOverallFOVDistance);

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner"))
        {
            partnerTransform = null;
            inMeleeFOV = false;
            inProjectileFOV = false;
        }
        else if (collision.CompareTag("Player"))
        {
            playerTransform = null;
            inMeleeFOV = false;
            inProjectileFOV = false;
        }
    }

    public bool IsPlayerInFieldOfView(Transform playerTransform, float fieldOfViewAngle, float detectionRange)
    {
        int playerAndPartnerLayerMask = whatIsPartner | whatIsPlayer;

        Vector2 directionToPlayer = enemy.enemyDirection;
        float angleToPlayer = Vector2.Angle(directionToPlayer, playerTransform.position - transform.position);

        if (angleToPlayer < fieldOfViewAngle * 0.5f)
        {
            RaycastHit2D[] results = new RaycastHit2D[10];
            int hits = Physics2D.RaycastNonAlloc(transform.position, directionToPlayer, results, detectionRange, playerAndPartnerLayerMask);
            for (int i = 0; i < hits; i++)
            {
                if (results[i].collider.CompareTag("Player") || results[i].collider.CompareTag("Partner"))
                {
                    return true;
                }
                else
                    return false;
            }
        }

        // Player is not in the field of view
        return false;
    }


    private void Update()
    {
        if (inMeleeFOV)
        {
            Debug.Log("MeleeFov working");
        }
        if (inProjectileFOV)
        {
            Debug.Log("ProjectileFOV working");
        }
    }

}
