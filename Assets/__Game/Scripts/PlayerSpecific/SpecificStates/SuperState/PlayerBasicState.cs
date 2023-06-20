using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicState : PlayerState
{
    protected int yInput;
    protected int xInput;

    protected bool switchInput;
    protected bool interactInput;
    protected bool evolveInput;

    protected bool isTouchingWall;
    protected bool isTouchingCarryable;
    protected bool isTouchingGround;
    protected bool canExitState;
    protected bool isTouchingPartner;
    protected RaycastHit2D Hits;
    protected RaycastHit2D HeldItemHit;
    protected Transform carryPoint;
    protected bool currentlyCarrying = false;
    public PlayerBasicState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(player, PSM, playerSOData, playerData, animBoolName)
    {
    }
    public override void DoChecks()
    {
        base.DoChecks();
        // TODO add references to collision senses
        carryPoint = PlayerCollisionSenses.carryPoint;
        Hits = PlayerCollisionSenses.Hits;
        HeldItemHit = PlayerCollisionSenses.HeldItemHit;
        isTouchingWall = PlayerCollisionSenses.WallCheck;
        isTouchingCarryable = PlayerCollisionSenses.CarryableCheck;
        isTouchingGround = PlayerCollisionSenses.GroundCheck;
        isTouchingPartner = PlayerCollisionSenses.PartnerCheck;
    }

    public override void Enter()
    {
        base.Enter();
        CameraSwitcher.SwitchCamera(player.PlayerCamera);
        statEvents.onCurrentEPZero += TimeToDevolve;
        player.evolutionEvents.OnStopForEvolution += StartEvolution;
    }

    public override void Exit()
    {
        base.Exit();
        statEvents.onCurrentEPZero -= TimeToDevolve;
        player.evolutionEvents.OnStopForEvolution -= StartEvolution;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        switchInput = player.InputHandler.SwitchPlayerInput;
        interactInput = player.InputHandler.InteractInput;
        evolveInput = player.InputHandler.EvolveInput;
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    

    void StartEvolution()
    {
        PSM.ChangeState(player.EvolutionState);
    }
}
