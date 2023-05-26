using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected float startTime;
    protected PlayerBasicData playerData;
    protected Player player;
    protected PlayerStateMachine PSM;
    protected bool isExitingState;
    string animBoolName;
    //Constructor    
    public PlayerState(Player player, PlayerStateMachine PSM, PlayerBasicData playerData, string animBoolName)
    {
        this.player = player;
        this.PSM = PSM;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time; //might need changed
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, true);

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
    
}
