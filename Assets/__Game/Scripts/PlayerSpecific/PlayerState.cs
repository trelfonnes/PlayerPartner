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
    protected StatEvents statEvents;
    protected bool isExitingState;
    protected bool isAnimationFinished;
    
    protected float startTime;
    string animBoolName;

   
    //protected Movement Movement { get => movement ??= core.GetCoreComponent<Movement>(); }
   

    protected Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    protected Defeated Defeated { get => defeated ?? core.GetCoreComponent(ref defeated); }
    protected Particles Particles { get => particles ?? core.GetCoreComponent(ref particles); }

  
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
        statEvents = player.statEvents;

    }

    public virtual void Enter()
    {
        statEvents.onPlayerHealthZero += PlayerIsDefeated;
        startTime = Time.time; //might need changed
                player.anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
        isExitingState = false;
        
    }

    public virtual void Exit()
    {
        if (player == null)
        {
            statEvents.onCurrentHealthZero -= PlayerIsDefeated;
            Debug.Log("Unsub because player was found to be null");
            return;
        }
        player.anim.SetBool(animBoolName, false);
        isExitingState = true;
        statEvents.onPlayerHealthZero -= PlayerIsDefeated;

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
    public virtual void TimeToDevolve()
    {
        if (player)
        {
            PSM.ChangeState(player.EvolutionState);
        }
    }
    public virtual void PlayerIsDefeated()
    {
        
            PSM.ChangeState(player.DefeatedState);
        
    }
    public virtual void OnDisable()
    {
        statEvents.onCurrentHealthZero -= PlayerIsDefeated;
    }
}
