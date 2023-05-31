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

    #endregion

    #region variable size that's accessible
    public float CollisionsCheckDistance { get => collisionCheckDistance; set => collisionCheckDistance = value; }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    #endregion

    #region Size of Checks
    [SerializeField] private float collisionCheckDistance = 1.5f;
    [SerializeField] private float groundCheckRadius = 0.3f;

    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private LayerMask whatIsCarryable;
    [SerializeField] private LayerMask whatIsGround;

    #endregion

    #region Public CollisionCheck Functions
    public bool WallCheck
    {
        get => Physics2D.Raycast(collisionsCheckPoint.position, player.playerDirection, collisionCheckDistance, whatIsWall);
    }public bool CarryableCheck
    {
        get => Physics2D.Raycast(collisionsCheckPoint.position, player.playerDirection, collisionCheckDistance, whatIsCarryable);
    }
    public bool GroundCheck
    {
        get => Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
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
