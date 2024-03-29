using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerFollowState : PartnerState
{
    protected int yInput;
    protected int xInput;

    protected bool switchInput;
    protected bool interactInput;
    protected bool evolveInput;

    protected bool isTouchingWall;
    protected bool isTouchingWallFollowing;
    protected bool isTouchingGround;
    protected bool canExitState;
    protected bool isTouchingPlayer;
    protected Transform player;
    protected PartnerCollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private PartnerCollisionSenses collisionSenses;


    public PartnerFollowState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        if (CollisionSenses)
        {
            isTouchingWallFollowing = CollisionSenses.WallCheckFollowing;
            isTouchingWall = CollisionSenses.WallCheckPartner;
            isTouchingGround = CollisionSenses.GroundCheck;
            isTouchingPlayer = CollisionSenses.PlayerCheck;
            player = CollisionSenses.followPoint;
        }
    }

    public override void Enter()
    {
        base.Enter();

       



    }

    public override void Exit()
    {
        base.Exit();
        

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = partner.InputHandler.NormInputX;
        yInput = partner.InputHandler.NormInputY;
        switchInput = partner.InputHandler.SwitchPlayerInput;
        interactInput = partner.InputHandler.InteractInput;
        evolveInput = partner.InputHandler.EvolveInput;
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    

}

