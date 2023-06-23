using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerCollisionSenses : CollisionSenses
{
    [SerializeField] public Transform followPoint;
    [SerializeField] private Transform playerCheckArea;

    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] private float playerCheckRadius = 1.5f;
    [SerializeField] private float wallCheckFollowRadius = 1f;
    public float PlayerDistanceFromPartner { get => playerCheckRadius; set => playerCheckRadius = value; }



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
    }
    public void EnableHazardDetection()
    {
        Physics2D.IgnoreLayerCollision(10, 15, false);

    }


}
