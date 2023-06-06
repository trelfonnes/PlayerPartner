using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicState : PlayerState
{
    protected int yInput;
    protected int xInput;

    protected bool switchInput;
    protected bool interactInput;

    protected bool isTouchingWall;
    protected bool isTouchingCarryable;
    protected bool isTouchingGround;
    protected bool canExitState;
    protected bool isTouchingPartner;
    public PlayerBasicState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
    {
    }
    public override void DoChecks()
    {
        base.DoChecks();
        // TODO add references to collision senses
          isTouchingWall = PlayerCollisionSenses.WallCheck;
         isTouchingCarryable = PlayerCollisionSenses.CarryableCheck;
         isTouchingGround = PlayerCollisionSenses.GroundCheck;
        isTouchingPartner = PlayerCollisionSenses.PartnerCheck;
    }

    public override void Enter()
    {
        base.Enter();
        CameraSwitcher.SwitchCamera(player.PlayerCamera);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        switchInput = player.InputHandler.SwitchPlayerInput;
        interactInput = player.InputHandler.InteractInput;
       
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
}
