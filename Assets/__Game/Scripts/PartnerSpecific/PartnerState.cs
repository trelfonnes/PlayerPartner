using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerState
{
    protected PlayerSOData playerSOData;
    protected CoreHandler core;
    protected Partner partner;
    protected PlayerStateMachine PSM;
    protected PlayerData playerData;
    protected bool isExitingState;
    protected bool isAnimationFinished;
    protected float startTime;
    string animBoolName;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected PartnerCollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    protected Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    protected Defeated Defeated { get => defeated ?? core.GetCoreComponent(ref defeated); }
    protected Particles Particles { get => particles ?? core.GetCoreComponent(ref particles); }

    
    private Movement movement;
    private PartnerCollisionSenses collisionSenses;
    private Stats stats;
    private Defeated defeated;
    private Particles particles;

    //Constructor    
    public PartnerState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName)
    {
        this.partner = partner;
        this.PSM = PSM;
        this.playerSOData = playerSOData;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core = partner.core;

    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time; //might need changed
        partner.anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
        isExitingState = false;

    }

    public virtual void Exit()
    {
        partner.anim.SetBool(animBoolName, false);
        isExitingState = true;

    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    void Start()
    {

    }
    public virtual void DoChecks()
    {

    }
    public virtual void AnimationTrigger()
    {

    }
    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    }

}
