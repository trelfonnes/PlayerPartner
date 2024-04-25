using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerAttackState : PartnerAbilityState
{
    PartnerWeapon weapon;
    int inputIndex;
    public PartnerAttackState(Partner partner, 
        PlayerStateMachine PSM, 
        PlayerSOData playerSOData, 
        PlayerData playerData, 
        string animBoolName,
        PartnerWeapon weapon,
        CombatInputs input) 
        : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
        this.weapon = weapon;
       // weapon.onExit += ExitHandler; //DO I need to unsub?? Try in ondisable
        Subscribe((handler) => weapon.onExit += handler, ExitHandler);

        // weapon.onDevolve += Devolve;
        Subscribe((handler) => weapon.onDevolve += handler, Devolve);

        inputIndex = (int)input;
    }
    protected Particles Particles { get => particles ?? core.GetCoreComponent(ref particles); }
    private Particles particles;

    void LevelUp()
    {
        Particles.StartParticles(ParticleType.LevelUp, core.transform.position, core.transform.rotation);
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
           // statEvents.onCurrentEPZero += Devolve;
            Subscribe((handler) => statEvents.onCurrentEPZero += handler, Devolve);


        }
      //  statEvents.onLevelUp += LevelUp;
        Subscribe((handler) => statEvents.onLevelUp += handler, LevelUp);
        Subscribe((handler) => statEvents.onCurrentHealthZero += handler, Partner1Defeated);

    }

    public override void Exit()
    {
        base.Exit();
        weapon.Exit();
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero -= Devolve;

        }
        statEvents.onLevelUp -= LevelUp;
        statEvents.onCurrentHealthZero -= Partner1Defeated;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        weapon.CurrentInput = partner.InputHandler.AttackInputs[inputIndex];
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
    public override void OnDisable()
    {
        base.OnDisable();
        weapon.onDevolve -= Devolve;
        statEvents.onLevelUp -= LevelUp;
        weapon.onExit -= ExitHandler;
        statEvents.onCurrentEPZero -= Devolve;
        statEvents.onCurrentHealthZero -= Partner1Defeated;
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
