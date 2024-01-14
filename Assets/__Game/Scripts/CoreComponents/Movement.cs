using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D rb { get; private set; }
    protected Vector2 workspace;
    public int facingDirectionX { get; private set; }
    public int facingCombatDirectionX { get; private set; }
    public int facingCombatDirectionY { get; private set; }
    public int facingDirectionY { get; private set; }
    Vector2 lastFacingCombatDirection;
    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 latestMovingVelocity { get; private set; }
    public bool CanSetVelocity { get; set; }
    CoreComp<TriReceiver> receiver;
    bool isKnockedback = false;
    float knockbackEndTime;
    protected bool canReceiveInput = true;
    [SerializeField] private float iceAccelerationMultiplier = 1.2f;
    [SerializeField] private float sandAccelerationMultiplier = 0.70f;
    [SerializeField] private float snowAccelerationMultiplier = 0.50f;
    [SerializeField] private float iceAccelerationLerpFactor = 0.5f;
    [SerializeField] private float sandAccelerationLerpFactor = 0.9f;
    [SerializeField] private float snowAccelerationLerpFactor = 1f;
    float decelerationRate;
    private Vector2 maxVelocity = Vector2.zero;


    protected override void Awake()
    {
        base.Awake();
        rb = GetComponentInParent<Rigidbody2D>();
        facingDirectionX = 1;
        facingDirectionY = -1;
        facingCombatDirectionX = 0;
        facingCombatDirectionY = -1;
        receiver = new CoreComp<TriReceiver>(core);
        CanSetVelocity = true;
    }
    public override void LogicUpdate()
    {
        CurrentVelocity = rb.velocity;
        // if(noInputDetected) 
        if (player)
        {
            if (!player.InputHandler.HasMoveInput)
            {
                workspace = Vector2.Lerp(workspace, Vector2.zero, Time.deltaTime * decelerationRate);
                SetFinalVelocity();
            }

            if (rb.velocity != Vector2.zero)
            {
                player.playerDirection = rb.velocity;
              
                CheckCombatHitBoxDirection(player.playerDirection.x, player.playerDirection.y);

            }
        }
        if (partner)
        {
            if (!partner.InputHandler.HasMoveInput)
            {
                workspace = Vector2.Lerp(workspace, Vector2.zero, Time.deltaTime * decelerationRate);
                SetFinalVelocity();
            }

            if (rb.velocity != Vector2.zero)
            {
                partner.playerDirection = rb.velocity;
              
                CheckCombatHitBoxDirection(partner.playerDirection.x, partner.playerDirection.y);

            }
        }
        
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

    public void CheckCombatHitBoxDirection(float xInput, float yInput) 
        {
        if (xInput > .06f)
        {
            facingCombatDirectionX = 1;
        }
        else if (xInput < -.06f)
        {
            facingCombatDirectionX = -1;
        }
        else 
        {
            facingCombatDirectionX = 0;
        }


        if (yInput > .06f)
        {
            facingCombatDirectionY = 1;
        }
        else if (yInput < -.06f)
        {
            facingCombatDirectionY = -1;
        }
        else
        {
            facingCombatDirectionY = 0;
        }
        if (!enemy)
        {
            if (facingCombatDirectionX == 0 && facingCombatDirectionY == 0)
            {
                facingCombatDirectionX = (int)lastFacingCombatDirection.x;
                facingCombatDirectionY = (int)lastFacingCombatDirection.y;
            }
            else
            {
                lastFacingCombatDirection = new Vector2(facingCombatDirectionX, facingCombatDirectionY);

            }
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


    public void SetVelocity(Vector2 direction, float speed) // for regular movement interactions on tiles
    {  
        if (canReceiveInput)
        {
            (float adjustedSpeed, float accelerationLerpFactor) = CalculateTileSpecificSpeed(speed);

            UpdateMaxVelocity(direction, adjustedSpeed);

            //float accelerationFactor = Mathf.Pow(1 - accelerationLerpFactor, Time.deltaTime * 60f);
           // Debug.Log(accelerationFactor);


            workspace = Vector2.Lerp(workspace, maxVelocity, Time.deltaTime * accelerationLerpFactor);
            SetFinalVelocity();
        }
    }
    private void UpdateMaxVelocity(Vector2 direction, float adjustedSpeed)
    {
        // Check if maxVelocity needs to be updated based on your conditions
        // For example, only update if adjustedSpeed or direction changes
        if (adjustedSpeed != maxVelocity.magnitude || direction != maxVelocity.normalized)
        {
            maxVelocity = direction * adjustedSpeed;
        }
    }
    private (float adjustedSpeed, float accelerationLerpFactor) CalculateTileSpecificSpeed(float baseSpeed)
    {
        float adjustedSpeed = baseSpeed;  //default speed will be returned if no conditions are met
        float accelerationLerpFactor = 100f; // Default value returned if no condition is met
        decelerationRate = accelerationLerpFactor;
        // decelerationRate = 1.0f;
        // Check for tile-specific conditions and adjust speed and lerpFactor accordingly
        if (CollisionSenses.isIceTile)
        {
            adjustedSpeed *= iceAccelerationMultiplier;
            accelerationLerpFactor = iceAccelerationLerpFactor;
            decelerationRate = iceAccelerationLerpFactor;

        }
        else if (CollisionSenses.isSandTile)
        {
            adjustedSpeed *= sandAccelerationMultiplier;
            accelerationLerpFactor = sandAccelerationLerpFactor;
            decelerationRate = sandAccelerationLerpFactor;

        }
        else if (CollisionSenses.isSnowTile)
        {
            adjustedSpeed *= snowAccelerationMultiplier;
            accelerationLerpFactor = snowAccelerationLerpFactor;
            decelerationRate = snowAccelerationLerpFactor;
        }
        // Add more conditions for other tile types if needed

        return (adjustedSpeed, accelerationLerpFactor);
    }

    public void SetVelocity(Vector2 velocity) //for attacks, abilities, special case movements don't interact with tiles.
    {
        if (canReceiveInput)
        {
            //workspace = direction * velocity;
            workspace = velocity;
            SetFinalVelocity();
        }
    }

    public void SetLatestVelocity(Vector2 velocity)//TODO: create a reference in partner move state as well
    {
        latestMovingVelocity = velocity;
    }
    public void SetKnockBackVelocity(Vector2 angle, float strength, int directionX, int directionY)
    {
        if (!isKnockedback && this.isActiveAndEnabled)
        {
            isKnockedback = true;
            canReceiveInput = false;
            workspace.Set(directionX * strength, directionY * strength); //dont thing I need angle for this game but good to have it here just in case
            SetFinalVelocity();
            knockbackEndTime = Time.time + .2f;
            StartCoroutine(EndKnockback());
        }
    }
    private IEnumerator EndKnockback()
    {
        while(Time.time < knockbackEndTime)
        {
            yield return null;
        }
        isKnockedback = false;
        canReceiveInput = true;

    }
    protected void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            rb.velocity = workspace;
            CurrentVelocity = workspace;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

}
