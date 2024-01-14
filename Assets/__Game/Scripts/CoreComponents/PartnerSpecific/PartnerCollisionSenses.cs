using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerCollisionSenses : CollisionSenses
{

    [SerializeField] public Transform followPoint;
    [SerializeField] public Transform maleFollowPoint;
    [SerializeField] public Transform femaleFollowPoint;
    [SerializeField] private Transform playerCheckArea;

    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] private float playerCheckRadius = 1.5f;
    [SerializeField] private float wallCheckFollowRadius = 1f;
    public float PlayerDistanceFromPartner { get => playerCheckRadius; set => playerCheckRadius = value; }

    bool isJumping;

    protected override void Start()
    {
        base.Start();
        if(GameManager.Instance.chosenPlayer == PlayerType.Male)
        {
            followPoint = maleFollowPoint;
        }
        else
        {
            followPoint = femaleFollowPoint;
        }
    }
    public bool PlayerCheck
    {
        get 
        {
            Collider2D[] results = new Collider2D[50];
            int count = Physics2D.OverlapCircleNonAlloc(playerCheckArea.position, playerCheckRadius, results, whatIsPlayer);
            return count > 0;
       
        }
    }
    public bool WallCheckFollowing
    {
        get
        {
            Collider2D[] results = new Collider2D[50];
            int count = Physics2D.OverlapCircleNonAlloc(playerCheckArea.position, wallCheckFollowRadius, results, whatIsWall);
            return count > 0;
        }
    }

    public void DisableHazardDetection()
    {
        Physics2D.IgnoreLayerCollision(10, 15, true); // 10 = partner, 15 = hazards
        Physics2D.IgnoreLayerCollision(10, 19, true); // 10 = partner, 19 = pitfalls
        isJumping = true;
    }
    public void EnableHazardDetection()
    {
        Physics2D.IgnoreLayerCollision(10, 15, false);
        Physics2D.IgnoreLayerCollision(10, 19, false);
        isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pitfall"))
        {

            partner.OnStartFallEvent();
        }
        if (GroundCheck)
        {
          
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
    
    

}
