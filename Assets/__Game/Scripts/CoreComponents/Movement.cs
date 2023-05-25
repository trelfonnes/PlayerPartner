using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    Rigidbody2D rb;
    private Vector2 workspace;
    public int facingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public bool canSetVelocity { get; set; }

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponentInParent<Rigidbody2D>();
        facingDirection = 1;
        canSetVelocity = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
