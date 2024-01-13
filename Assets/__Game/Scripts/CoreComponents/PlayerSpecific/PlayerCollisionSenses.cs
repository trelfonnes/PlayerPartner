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
        Collider2D tileCollider = GetTileColliderAtPlayerPosition();

        if (tileCollider != null)
        {
            Debug.Log("PlayerCollisionSenses: Initialized inside tile: " + tileCollider.name);

            if (tileCollider.CompareTag("IceTile"))
            {
                isIceTile = true;
                isSandTile = false;
                isSnowTile = false;
            }
            else if (tileCollider.CompareTag("SnowTile"))
            {
                isSnowTile = true;
                isIceTile = false;
                isSandTile = false;
            }
            else if (tileCollider.CompareTag("SandTile"))
            {
                isSandTile = true;
                isIceTile = false;
                isSnowTile = false;
            }
            // ... (repeat for other tile types)
        }
    }
    private Collider2D GetTileColliderAtPlayerPosition()
    {
        
            Vector2 playerPosition = player.transform.position;
            Collider2D tileCollider = Physics2D.OverlapPoint(playerPosition, whatIsGround);

            return tileCollider;
        

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
        Debug.Log("Collider detected in on collision" + collision.gameObject.name);
        if (GroundCheck)
        {
            if (collision.CompareTag("Pitfall"))
            {

                player.OnStartFallEvent();
            }
            if (collision.CompareTag("IceTile"))
            {
                Debug.Log("IceTile Detected");
                isIceTile = true;
                isSandTile = false;
                isSnowTile = false;
            }
            else if (collision.CompareTag("SnowTile"))
            {
                isSnowTile = true;
                isSandTile = false;
                isIceTile = false;
            }
            else if (collision.CompareTag("SandTile"))
            {
                isSandTile = true;
                isSnowTile = false;
                isIceTile = false;
            }
            else
            {
                isSandTile = false;
                isSnowTile = false;
                isIceTile = false;
            }
        }
    }


   
    private void OnTriggerExit2D(Collider2D collision)
    {
      //  Debug.Log("On trigger exit detected in collisionsensesPlayer");
     //   if (collision.CompareTag("IceTile"))
    //    {
     //     isIceTile = false;

       // }
      //  if (collision.CompareTag("SnowTile"))
      //  {
      //      isSnowTile = false;

     //   }
      //  if (collision.CompareTag("SandTile"))
      //  {
       //     isSandTile = false;


     //   }
    }

}
