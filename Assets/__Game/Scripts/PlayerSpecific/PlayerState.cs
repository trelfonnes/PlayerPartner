using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerBasicData playerData;
    protected CoreHandler core;
    protected Player player;
    protected PlayerStateMachine PSM;
    protected bool isExitingState;
    protected bool isAnimationFinished;
    protected float startTime;
    string animBoolName;

    protected Movement Movement {get => movement ?? core.GetCoreComponent(ref movement);}
   
    private Movement movement;


    //Constructor    
    public PlayerState(Player player, PlayerStateMachine PSM, PlayerBasicData playerData, string animBoolName)
    {
        this.player = player;
        this.PSM = PSM;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core = player.core;

        Debug.Log(core.name);
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
        player.anim.SetBool(animBoolName, true);
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
