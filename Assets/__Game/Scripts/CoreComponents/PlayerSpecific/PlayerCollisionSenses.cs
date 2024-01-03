using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionSenses : CollisionSenses
{
    [SerializeField]
    private Transform partnerCheckArea;
    [SerializeField]
    public Transform carryPoint;
    [SerializeField]
    float heldItemCheckDistance;
    [SerializeField] Transform pitfallCheckPoint;
    public bool PitFallCheck;
    protected override void Start()
    {
        base.Start();

    }

    public RaycastHit2D HitsToCarry
    {
        get
        {
            hitsToCarry = Physics2D.Raycast(collisionsCheckPoint.position, player.playerDirection, collisionCheckDistance, whatIsCarryable);

            return hitsToCarry;
        }
    }public RaycastHit2D HitsToInteract
    {
        get
        {
            hitsToInteract = Physics2D.Raycast(collisionsCheckPoint.position, player.playerDirection, collisionCheckDistance, whatIsInteractable);

            return hitsToInteract;
        }
    }
    public RaycastHit2D HeldItemHit
    {
        get
        {
            heldItemHit = Physics2D.Raycast(carryPoint.position, Vector2.up, heldItemCheckDistance, whatIsCarryable);
            return heldItemHit;
        }

    }

    private RaycastHit2D hitsToCarry;
    private RaycastHit2D hitsToInteract;
    private RaycastHit2D heldItemHit;
    public float PartnerDistanceFromPlayer { get => partnerDistanceFromPlayerRadius; set => partnerDistanceFromPlayerRadius = value; }
    [SerializeField] private float partnerDistanceFromPlayerRadius = 1.5f;

    [SerializeField] private LayerMask whatIsPartner;
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private LayerMask whatIsCarryable;
    [SerializeField] float partnerCheckRadius = .5f;
    [SerializeField] private float pitfallCheckDistance;

    public bool PartnerCheck
    {
        get => Physics2D.OverlapCircle(partnerCheckArea.position, partnerCheckRadius, whatIsPartner);

       // get => Physics2D.Raycast(partnerCheckArea.position, player.playerDirection, partnerDistanceFromPlayerRadius, whatIsPartner);
    }
    public bool CarryableCheck
    {
            
        get => Physics2D.Raycast(collisionsCheckPoint.position, player.playerDirection, collisionCheckDistance, whatIsCarryable);
          
    } 
    public bool InteractableCheck
    {
            
        get => Physics2D.Raycast(collisionsCheckPoint.position, player.playerDirection, collisionCheckDistance, whatIsInteractable);
          
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pitfall"))
        {
            Debug.Log("Fell in hole");

            player.OnStartFallEvent();
        }
    }
 


}
