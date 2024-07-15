using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerIdleState : PartnerBasicState
{
    bool subToLevelUP;
    protected PartnerCollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private PartnerCollisionSenses collisionSenses;
    public PartnerIdleState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName) : base(partner, PSM, playerSOData, playerData, animBoolName)
    {
    }

    protected Particles Particles { get => particles ?? core.GetCoreComponent<Particles>(); }
    private Particles particles;

    void LevelUp()
    {
        Particles.StartParticles(ParticleType.LevelUp, core.transform.position, core.transform.rotation);
    }
    void SubToLevelUp()
    {
        //statEvents.onLevelUp += LevelUp;
        Subscribe((handler) => statEvents.onLevelUp += handler, LevelUp);

        subToLevelUP = true;

    }
    public override void Enter()
    {
        base.Enter();
        canExitState = false;
        if (playerSOData.stage2 || playerSOData.stage3)
        {
          //  statEvents.onCurrentEPZero += TimeToDevolve;
            Subscribe((handler) => statEvents.onCurrentEPZero += handler, TimeToDevolve);

        }
        //partner.onFallStarted += StartFalling;
        Subscribe((handler) => partner.onFallStarted += handler, StartFalling);
        Subscribe((handler) => statEvents.onCurrentHealthZero += handler, Partner1Defeated);

    }

    public override void Exit()
    {
        base.Exit();
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero -= TimeToDevolve;

        }
        statEvents.onLevelUp -= LevelUp;
        subToLevelUP = false;
        partner.onFallStarted -= StartFalling;
        statEvents.onCurrentHealthZero -= Partner1Defeated;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (playerSOData.stage2 || playerSOData.stage3)
        {
            statEvents.onCurrentEPZero -= TimeToDevolve;

        }
        statEvents.onLevelUp -= LevelUp;
        partner.onFallStarted -= StartFalling;
        statEvents.onCurrentHealthZero -= Partner1Defeated;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (xInput != 0 || yInput != 0)
        {
            PSM.ChangePartnerState(partner.MoveState);
        }
        if(!switchInput && !interactInput)
        {
            canExitState = true;
        }


        if (canExitState)
        {
            if (switchInput)
            {
                PSM.ChangePartnerState(partner.FollowIdleState);
                partner.evolutionEvents.SwitchToPlayer();
            }
            //TODO behavior to interact input conditions
        }
        if (primaryAttackInput)
        {
            PSM.ChangePartnerState(partner.PrimaryAttackState);
        }
        if (secondaryAttackInput)
        {
            PSM.ChangePartnerState(partner.SecondaryAttackState);

        }
        if (!subToLevelUP)
        {
            SubToLevelUp();
        }
    
    }
  
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
