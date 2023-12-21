using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicState : PlayerState
{
    protected PlayerCollisionSenses PlayerCollisionSenses { get => playerCollisionSenses ?? core.GetCoreComponent(ref playerCollisionSenses); }
    private PlayerCollisionSenses playerCollisionSenses;



    protected int yInput;
    protected int xInput;

    protected bool switchInput;
    protected bool interactInput;
    protected bool evolveInput;
    protected bool primaryAttackInput;
    protected bool secondaryAttackInput;
    protected bool isWatching;
    protected bool isTouchingWall;
    protected bool isTouchingCarryable;
    protected bool isTouchingInteractable;
    protected bool isTouchingGround;
    protected bool isTouchingPitfall;
    protected bool canExitState;
    protected bool isTouchingPartner;
    protected RaycastHit2D HitsToCarry;
    protected RaycastHit2D HitsToInteract;
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
            HitsToCarry = PlayerCollisionSenses.HitsToCarry;
            HitsToInteract = PlayerCollisionSenses.HitsToInteract;
            HeldItemHit = PlayerCollisionSenses.HeldItemHit;
            isTouchingWall = PlayerCollisionSenses.WallCheck;
            isTouchingCarryable = PlayerCollisionSenses.CarryableCheck;
            isTouchingGround = PlayerCollisionSenses.GroundCheck;
            isTouchingPartner = PlayerCollisionSenses.PartnerCheck;
            isTouchingInteractable = PlayerCollisionSenses.InteractableCheck;
            isTouchingPitfall = PlayerCollisionSenses.PitFallCheck;
        
       
    } 

    public override void Enter()
    {
        base.Enter();
        player.evolutionEvents.OnStopForEvolution += StartEvolution;

        if (player)
        {
            CameraSwitcher.SwitchCamera(player.PlayerCamera, player.transform);
        }
            statEvents.onCurrentEPZero += TimeToDevolve;
        
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
        if (player.InputHandler)
        {
            xInput = player.InputHandler.NormInputX;
            yInput = player.InputHandler.NormInputY;
            switchInput = player.InputHandler.SwitchPlayerInput;
            interactInput = player.InputHandler.InteractInput;
            evolveInput = player.InputHandler.EvolveInput;
            if (player.InputHandler.AttackInputs != null)
            {
                primaryAttackInput = player.InputHandler.AttackInputs[(int)CombatInputs.primary];
                secondaryAttackInput = player.InputHandler.AttackInputs[(int)CombatInputs.secondary];
            }
        }

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


    void StartEvolution()
    {
        if (player)
        {
            PSM.ChangeState(player.EvolutionState);
        }
    }
}
