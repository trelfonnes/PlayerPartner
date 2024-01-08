using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerFollowIdleState : PartnerFollowState
{
    public PartnerFollowIdleState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
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
        partner.evolutionEvents.OnSwitchToPartner += BackToIdle;
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero += TimeToDevolve;

        }


    }

    public override void Exit()
    {
        base.Exit();
        partner.evolutionEvents.OnSwitchToPartner -= BackToIdle;
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero -= TimeToDevolve;

        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        partner.evolutionEvents.OnSwitchToPartner -= BackToIdle;
        Debug.Log("Unsub from switch to partner event");

        statEvents.onCurrentEPZero -= TimeToDevolve;

    } 

    private void BackToIdle()
    {
            PSM.ChangePartnerState(partner.IdleState);
         
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isTouchingPlayer && !isTouchingWallFollowing)
        {
            PSM.ChangePartnerState(partner.FollowMoveState);
        }
        if(!switchInput && !interactInput)
        {
            canExitState = true;
        }
        
        if (canExitState)
        {
            
            
               
            
        }
        if(evolveInput && isTouchingPlayer && !playerSOData.stage3 )
        {
            if (playerSOData.stage1 && playerData.deviceOneCollected && playerData.currentBondLevel >= playerSOData.bondToEvolveOne && playerData.ep >= 30)
            {

                PSM.ChangePartnerState(partner.EvolutionState);
            }
            else if(playerSOData.stage2 && playerData.deviceTwoCollected && playerData.currentBondLevel >= playerSOData.bondToEvolveTwo && playerData.ep >=30)
            {
                PSM.ChangePartnerState(partner.EvolutionState);

            }
        }
       

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
}
