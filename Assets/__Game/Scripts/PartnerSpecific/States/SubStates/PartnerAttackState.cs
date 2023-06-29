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
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        weapon.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
