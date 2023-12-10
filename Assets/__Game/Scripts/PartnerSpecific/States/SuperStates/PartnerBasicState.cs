using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerBasicState : PartnerState
{
    protected int yInput;
    protected int xInput;


    protected bool switchInput;
    protected bool interactInput;
    protected bool evolveInput;
    protected bool dashInput;
    protected bool primaryAttackInput;
    protected bool secondaryAttackInput;

    protected bool isTouchingWall;
    protected bool isTouchingWallFollowing; 
    protected bool isTouchingGround;
    protected bool isTouchingPitfall;
    protected bool canExitState;
    protected bool isTouchingPlayer;
    protected bool inBasicStates;
    protected Transform player;
    protected PartnerCollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private PartnerCollisionSenses collisionSenses;

    public PartnerBasicState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
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
        
            isTouchingWallFollowing = CollisionSenses.WallCheckFollowing;
            isTouchingWall = CollisionSenses.WallCheckPartner;
            isTouchingGround = CollisionSenses.GroundCheck;
            isTouchingPlayer = CollisionSenses.PlayerCheck;
            player = CollisionSenses.followPoint;
        
    }

    public override void Enter()
    {
        base.Enter();
        if (partner)
        {
            if (partner.PartnerCamera != null) 
            {
                CameraSwitcher.SwitchCamera(partner.PartnerCamera, partner.transform);
            }
        }
            inBasicStates = true;


    }

    public override void Exit()
    {
        base.Exit();
        inBasicStates = false;
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        primaryAttackInput = partner.InputHandler.AttackInputs[(int)CombatInputs.primary];
        secondaryAttackInput = partner.InputHandler.AttackInputs[(int)CombatInputs.secondary];
        xInput = partner.InputHandler.NormInputX;
        yInput = partner.InputHandler.NormInputY;
        switchInput = partner.InputHandler.SwitchPlayerInput;
        interactInput = partner.InputHandler.InteractInput;
        evolveInput = partner.InputHandler.EvolveInput;
        dashInput = partner.InputHandler.DashInput;
        if(partner.JumpCooldownTimer.IsFinished() && interactInput && partner.JumpState.CanJump() && playerSOData.canJump) //might need to change input to make it a hold or a double tap??
        {
            if(playerSOData.Stamina >=10 && !isTouchingPitfall)
            PSM.ChangePartnerState(partner.JumpState);
        }
        
        if (partner.DashCooldownTimer.IsFinished() && dashInput && partner.DashState.CanDash() && playerSOData.canDash) // and can dash 
        {
            if (playerSOData.Stamina >= 10 && !isTouchingPitfall)
                PSM.ChangePartnerState(partner.DashState);
             
        }
       
       
        

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
