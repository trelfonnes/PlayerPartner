using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerFollowMoveState : PartnerFollowState
{
    public PartnerFollowMoveState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
    }
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    public override void DoChecks()
    {
        base.DoChecks();
        
    }

    public override void Enter()
    {
        base.Enter();
        canExitState = false;
       // partner.evolutionEvents.OnSwitchToPartner += BackToIdle;
        Subscribe((handler) => partner.evolutionEvents.OnSwitchToPartner += handler, BackToIdle);
        Subscribe((handler) => statEvents.onCurrentHealthZero += handler, Partner1Defeated);

        if (playerSOData.stage2 || playerSOData.stage3)
        {
            Subscribe((handler) => statEvents.onCurrentEPZero += handler, TimeToDevolve);
           // statEvents.onCurrentEPZero += TimeToDevolve;

        }


    }

    public override void Exit()
    {
        base.Exit();
        partner.evolutionEvents.OnSwitchToPartner -= BackToIdle;
        statEvents.onCurrentHealthZero -= Partner1Defeated;
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero -= TimeToDevolve;

        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player != null)
        {
            var direction = (player.position - partner.transform.position).normalized;
            //direction.Normalize();
            Movement?.CheckIfShouldFlipFollowing(direction);


            if (!isTouchingPlayer && !isTouchingWallFollowing)
            {
                partner.transform.position += direction * playerSOData.followSpeed * Time.deltaTime;



            }
            if (direction.x != 0 && direction.y != 0)
            {
                partner.anim.SetFloat("moveY", Mathf.Round(direction.y));
                partner.anim.SetFloat("moveX", Mathf.Round(direction.x));
            }

        }

        if (isTouchingPlayer || isTouchingWallFollowing)
        {
            Movement?.SetVelocityZero();
            PSM.ChangePartnerState(partner.FollowIdleState);
        }
        
        if(!switchInput && !interactInput)
        {
            canExitState = true;
        }
        
      
        

    }
    public override void OnDisable()
    {
        base.OnDisable();
        statEvents.onCurrentEPZero -= TimeToDevolve;
        partner.evolutionEvents.OnSwitchToPartner -= BackToIdle;
        statEvents.onCurrentHealthZero -= Partner1Defeated;

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void BackToIdle()
    {
        Debug.Log("Back to idle");
        PSM.ChangePartnerState(partner.IdleState);
    }
}

