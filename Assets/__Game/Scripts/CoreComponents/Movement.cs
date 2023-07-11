using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D rb { get; private set; }
    private Vector2 workspace;
    public int facingDirectionX { get; private set; }
    public int facingCombatDirectionX { get; private set; }
    public int facingCombatDirectionY { get; private set; }
    public int facingDirectionY { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 latestMovingVelocity { get; private set; }
    public bool CanSetVelocity { get; set; }

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponentInParent<Rigidbody2D>();
        facingDirectionX = 1;
        facingDirectionY = -1;
        facingCombatDirectionX = 0;
        facingCombatDirectionY = -1;

        CanSetVelocity = true;
    }
    public override void LogicUpdate()
    {
        CurrentVelocity = rb.velocity;
    }
    public void FlipX()
    {
        facingDirectionX *= -1;
        rb.transform.Rotate(0.0f, 180.0f, 0.0f);

    }
    public void FlipY()
    {
        //facingDirectionX *= -1;
        //rb.transform.Rotate(180.0f, 0.0f, 0.0f);

    }
    public void CheckIfShouldFlip(int xInput, int yInput)
    {
        if (xInput != 0 && xInput != facingDirectionX)
        {
            FlipX();
        }
    }

    public void CheckCombatHitBoxDirection(int xInput, int yInput) 
        {
        if (xInput > 0)
        {
            facingCombatDirectionX = 1;
        }
        else if (xInput < 0)
        {
            facingCombatDirectionX = -1;
        }
        else if (xInput == 0)
        {
            facingCombatDirectionX = 0;
        }


        if (yInput > 0)
        {
            facingCombatDirectionY = 1;
        }
        else if (yInput < 0)
        {
            facingCombatDirectionY = -1;
        }
        else if( yInput == 0)
        {
            facingCombatDirectionY = 0;
        }

    }
    public void CheckIfShouldFlipFollowing(Vector2 vector2)
    {
        vector2.x = Mathf.Round(vector2.x);
        if(vector2.x != 0 && vector2.x != facingDirectionX)
        {
            FlipX();
        }
        

    }

    public void SetVelocityZero()
    {
        workspace = Vector2.zero;
        SetFinalVelocity();
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }
    public void SetVelocity(Vector2 velocity)
    {
        //workspace = direction * velocity;
        workspace = velocity;
        SetFinalVelocity();
    }

    public void SetLatestVelocity(Vector2 velocity)
    {
        latestMovingVelocity = velocity;
    }

    private void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            rb.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }

}
