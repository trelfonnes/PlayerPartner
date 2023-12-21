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
    protected StatEvents statEvents;
    protected bool isExitingState;
    protected bool isAnimationFinished;
    protected bool isDevolvingAbilityCancel;
    //protected bool epAtZero = false;
    protected float startTime;
    string animBoolName;

    // protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
    //protected PartnerCollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }



    protected Stats Stats { get => stats ??= core.GetCoreComponent<Stats>(); }
    protected Defeated Defeated { get => defeated ?? core.GetCoreComponent(ref defeated); }
  


    private Stats stats;
    private Defeated defeated;

    //Constructor    
    public PartnerState(Partner partner, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName)
    {
        this.partner = partner;
        this.PSM = PSM;
        this.playerSOData = playerSOData;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core = partner.core;
        statEvents = partner.statEvents; //refactor to fit in constructor if it works
    }

    public virtual void Enter()
    {
        //DoChecks();
        startTime = Time.time; //might need changed
        partner.anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
        isExitingState = false;
        statEvents.onCurrentHealthZero += Partner1Defeated;

    }

    public virtual void Exit()
    {
        Debug.Log(this.GetType().Name); 
        partner.anim.SetBool(animBoolName, false);
        isExitingState = true;
        statEvents.onCurrentHealthZero -= Partner1Defeated;


    }
    public virtual void OnDisable()
    {
        statEvents.onCurrentHealthZero -= Partner1Defeated;
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
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
    public virtual void TimeToDevolve()
    {

        if (!playerSOData.stage1)
        {

            PSM.ChangePartnerState(partner.DevolveState);
        }

    }
    public void StartFalling()
    {
        Debug.Log("Partner going to fall state");
        
        PSM.ChangePartnerState(partner.FallingState);
    }
    public virtual void Partner1Defeated()
    {
        if (playerSOData.stage1)
        {
            PSM.ChangePartnerState(partner.DefeatedState);
        }
    }
}
