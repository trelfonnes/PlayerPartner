using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check for Error
    //public Transform WallCheck
    //{
      //  get
        //{
          //  return GenericNotImplementedError<Transform>.TryGet
            //    (WallCheck, core.transform.parent.name);
        //}
        //private set => wallCheck = value; }
    #endregion

    #region Serialized Transforms
    [SerializeField]
    private Transform collisionsCheckPoint; 
    [SerializeField]
    private Transform groundCheckPoint;
    [SerializeField]
    private Transform partnerFollowPoint;
    

    #endregion

    #region variable size that's accessible
    public float CollisionsCheckDistance { get => collisionCheckDistance; set => collisionCheckDistance = value; }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float PartnerDistanceFromPlayer { get => partnerDistanceFromPlayer; set => partnerDistanceFromPlayer = value;}
    public float PlayerDistanceFromPartner { get => playerDistanceFromPartner; set => playerDistanceFromPartner = value; }
    #endregion

    #region Size of Checks
    [SerializeField] private float collisionCheckDistance = 1.5f;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private float partnerDistanceFromPlayer = 1.5f;
    [SerializeField] private float playerDistanceFromPartner = 1.5f;

    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsCarryable;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPartner;
    [SerializeField] private LayerMask whatIsPlayer;

    #endregion

    #region Public CollisionCheck Functions
    public bool WallCheck
    {
        get => Physics2D.Raycast(collisionsCheckPoint.position, player.playerDirection, collisionCheckDistance, whatIsWall);
    } 
    public bool WallCheckPartner
    {
        get => Physics2D.Raycast(collisionsCheckPoint.position, partner.playerDirection, collisionCheckDistance, whatIsWall);
    }
    public bool CarryableCheck
    {
        get => Physics2D.Raycast(collisionsCheckPoint.position, player.playerDirection, collisionCheckDistance, whatIsCarryable);
    }
    public bool GroundCheck
    {
        get => Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
    }
    public bool PartnerCheck
    {
        get => Physics2D.Raycast(partnerFollowPoint.position, -player.playerDirection, partnerDistanceFromPlayer, whatIsPartner);    
    }
    public bool PlayerCheck
    {
        get => Physics2D.Raycast(collisionsCheckPoint.position, partner.playerDirection, playerDistanceFromPartner, whatIsPlayer);
    }
    #endregion





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = new Vector3(0, -1, 0);
        Gizmos.DrawLine(collisionsCheckPoint.position, collisionsCheckPoint.position + direction * collisionCheckDistance);

        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
