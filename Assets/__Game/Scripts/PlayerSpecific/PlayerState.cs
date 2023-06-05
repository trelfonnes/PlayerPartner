using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerSOData playerSOData;
    protected CoreHandler core;
    protected Player player;
    protected PlayerStateMachine PSM;
    protected PlayerData playerData;
    protected bool isExitingState;
    protected bool isAnimationFinished;
    protected float startTime;
    string animBoolName;

    protected Movement Movement {get => movement ?? core.GetCoreComponent(ref movement);}
    protected PlayerCollisionSenses PlayerCollisionSenses
    {
        get
        {
            if (playerCollisionSenses == null)
            {
                playerCollisionSenses = core.GetCoreComponent(ref playerCollisionSenses);
            }
            return playerCollisionSenses;
        }
    }
    protected Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    protected Defeated Defeated { get => defeated ?? core.GetCoreComponent(ref defeated); }
    protected Particles Particles { get => particles ?? core.GetCoreComponent(ref particles); }

    private Movement movement;
    private PlayerCollisionSenses playerCollisionSenses;
    private Stats stats;
    private Defeated defeated;
    private Particles particles;

    //Constructor    
    public PlayerState(Player player, PlayerStateMachine PSM, PlayerSOData playerSOData, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.PSM = PSM;
        this.playerSOData = playerSOData;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core = player.core;

    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time; //might need changed
        player.anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
        isExitingState = false;
        
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
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
