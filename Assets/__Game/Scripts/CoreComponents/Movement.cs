using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D rb { get; private set; }
    private Vector2 workspace;
    public int facingDirectionX { get; private set; }
    public int facingDirectionY { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public bool CanSetVelocity { get; set; }

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponentInParent<Rigidbody2D>();
        facingDirectionX = 1;
        facingDirectionY = -1;

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
        if(xInput != 0 && xInput != facingDirectionX)
        {
            FlipX();
        }
        if(yInput != 0 && yInput != facingDirectionY)
        {
            FlipY();
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
    public void SetVelocity(float velocityX, float velocityY)
    {
        Debug.Log("settingVelocity");
        //workspace = direction * velocity;
        workspace.Set(velocityX, velocityY);
        SetFinalVelocity();
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
