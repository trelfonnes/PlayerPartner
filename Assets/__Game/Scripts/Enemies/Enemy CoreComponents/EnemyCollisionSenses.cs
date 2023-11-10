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
    [SerializeField] protected float RangeAttkCheckDistance = 4f;
    [SerializeField] protected float MeleeAttkCheckDistance = 1f;
    [SerializeField] protected float CharacterCheckRadius = 8f;

  
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
   

    public bool PartnerCheck
    {
        get => Physics2D.Raycast(collisionsCheckPoint.position, enemy.enemyDirection, collisionCheckDistance, whatIsPartner);

    }   
    public bool PlayerCheck
    {
        get => Physics2D.Raycast(collisionsCheckPoint.position, enemy.enemyDirection, collisionCheckDistance, whatIsPlayer);

    }
    public bool RangedAttackCheck
    {
        get => Physics2D.Raycast(collisionsCheckPoint.position, enemy.enemyDirection, RangeAttkCheckDistance, whatIsPlayer, whatIsPartner);

    }  
    public bool MeleeAttackCheck
    {
        get => Physics2D.Raycast(collisionsCheckPoint.position, enemy.enemyDirection, MeleeAttkCheckDistance, whatIsPlayer, whatIsPartner);

    }
    public bool SightCircle
    {
        get
        {
            Collider2D[]
            results = new Collider2D[50];
            int count = Physics2D.OverlapCircleNonAlloc(groundCheckPoint.position, CharacterCheckRadius, results, whatIsPlayer, whatIsPartner);
            return count > 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner"))
        {
            partnerTransform = collision.transform;
        }
        else if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner"))
        {
            partnerTransform = null;
        }
        else if (collision.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }

}
