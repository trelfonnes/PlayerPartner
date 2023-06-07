using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionSenses : CollisionSenses
{
    [SerializeField]
    private Transform partnerCheckArea;
    [SerializeField]
    public Transform carryPoint;
    public RaycastHit2D Hits
    {
        get
        {
            hits = Physics2D.Raycast(collisionsCheckPoint.position, player.playerDirection, collisionCheckDistance, whatIsCarryable);

            return hits;
        }
    }
    private RaycastHit2D hits;

    public float PartnerDistanceFromPlayer { get => partnerDistanceFromPlayerRadius; set => partnerDistanceFromPlayerRadius = value; }
    [SerializeField] private float partnerDistanceFromPlayerRadius = 1.5f;

    [SerializeField] private LayerMask whatIsPartner;

    [SerializeField] private LayerMask whatIsCarryable;
    public bool PartnerCheck
    {
        get => Physics2D.Raycast(partnerCheckArea.position, player.playerDirection, partnerDistanceFromPlayerRadius, whatIsPartner);
    }
    public bool CarryableCheck
    {
            
        get => Physics2D.Raycast(collisionsCheckPoint.position, player.playerDirection, collisionCheckDistance, whatIsCarryable);
          
    }
   

}
