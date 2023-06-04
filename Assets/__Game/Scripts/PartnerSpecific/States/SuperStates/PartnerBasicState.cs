using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerBasicState : PartnerState
{
    protected int yInput;
    protected int xInput;

    protected bool switchInput;
    protected bool interactInput;

    protected bool isTouchingWall;
    protected bool isTouchingGround;
    protected bool canExitState;
    protected bool isTouchingPlayer;
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
        isTouchingWall = CollisionSenses.WallCheckPartner;
        isTouchingGround = CollisionSenses.GroundCheck;
        isTouchingPlayer = CollisionSenses.PlayerCheck;

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


        if (isTouchingPlayer)
        {
            Debug.Log("istouchingPLAYER");
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
