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
    protected Transform collisionsCheckPoint; 
    [SerializeField]
    protected Transform groundCheckPoint;
  
   

    #endregion

    #region variable size that's accessible
    public float CollisionsCheckDistance { get => collisionCheckDistance; set => collisionCheckDistance = value; }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    #endregion

    #region Size of Checks
    [SerializeField] protected float collisionCheckDistance = 1.25f;
    [SerializeField] private float groundCheckRadius = 0.3f;

    [SerializeField] protected LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsPitfall;

    #endregion

    #region Public CollisionCheck Functions
    public bool WallCheck
    {
        get
        {
            RaycastHit2D[] results = new RaycastHit2D[50];
            int hits = Physics2D.RaycastNonAlloc(collisionsCheckPoint.position, player.playerDirection, results, collisionCheckDistance, whatIsWall);
            return hits > 0;
        }
    }
  
    
    public bool WallCheckPartner
    {
        get
        {
            RaycastHit2D[] results = new RaycastHit2D[100];
            int hits = Physics2D.RaycastNonAlloc(collisionsCheckPoint.position, partner.playerDirection, results, collisionCheckDistance, whatIsWall);
            return hits > 0;
        }
        }

    public bool GroundCheck
    {
        get
        {
            Collider2D[] results = new Collider2D[50];
            int count = Physics2D.OverlapCircleNonAlloc(groundCheckPoint.position, groundCheckRadius, results, whatIsGround);
            return count > 0;
        }
    }
   

    #endregion





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = new Vector3(0, -1, 0);
        Gizmos.DrawLine(collisionsCheckPoint.position, collisionsCheckPoint.position + direction * collisionCheckDistance);

    }
}
