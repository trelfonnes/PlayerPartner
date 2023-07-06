using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerAttackState : PartnerAbilityState
{
    PartnerWeapon weapon;
    public PartnerAttackState(Partner partner, 
        PlayerStateMachine PSM, 
        PlayerSOData playerSOData, 
        PlayerData playerData, 
        string animBoolName,
        PartnerWeapon weapon) 
        : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
        this.weapon = weapon;
        weapon.onExit += ExitHandler; //DO I need to unsub??
        weapon.onDevolve += Devolve;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        weapon.Enter();
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero += Devolve;

        }

    }

    public override void Exit()
    {
        base.Exit();
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero -= Devolve;

        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    void ExitHandler()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
    void Devolve()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
        isDevolvingAbilityCancel = true;
        if (!playerSOData.stage1)
        {
            Debug.Log("DEVOLVE");
            PSM.ChangePartnerState(partner.DevolveState);
        }
    }
}
