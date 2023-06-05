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
        get => Physics2D.OverlapCircle(playerCheckArea.position, playerCheckRadius, whatIsPlayer);
    }
    public bool WallCheckFollowing
    {
        get => Physics2D.OverlapCircle(playerCheckArea.position, wallCheckFollowRadius, whatIsWall);
    }
}
