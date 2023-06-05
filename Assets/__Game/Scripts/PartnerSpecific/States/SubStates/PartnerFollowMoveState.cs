using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerFollowMoveState : PartnerFollowState
{
    public PartnerFollowMoveState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        canExitState = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        var direction = (player.position - partner.transform.position).normalized;
        //direction.Normalize();
        Movement?.CheckIfShouldFlipFollowing(direction);


        if (!isTouchingPlayer && !isTouchingWallFollowing)
        {
            partner.transform.position += direction * playerSOData.followSpeed * Time.deltaTime;
            
            

        }
        if(direction.x != 0 && direction.y != 0)
        {
            partner.anim.SetFloat("moveY", Mathf.Round(direction.y));
            partner.anim.SetFloat("moveX", Mathf.Round(direction.x));
        }



        if (isTouchingPlayer || isTouchingWallFollowing)
        {
            Movement.SetVelocityZero();
            PSM.ChangePartnerState(partner.FollowIdleState);
        }
        
        if(!switchInput && !interactInput)
        {
            canExitState = true;
        }
        
       if (canExitState)
       {
            if (switchInput)
            {
                PSM.ChangePartnerState(partner.IdleState);
            }
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

