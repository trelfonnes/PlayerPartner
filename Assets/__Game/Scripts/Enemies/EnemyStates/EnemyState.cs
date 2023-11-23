using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemySOData enemySoData;
    protected CoreHandler core;
    protected Enemy enemy;
    protected EnemyStateMachine ESM;
    protected EnemyData data;
    protected bool isExitingState;
    protected bool isAnimationFinished;

    protected float startTime;
    string animBoolName;


    public EnemyState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, EnemyData data, string animBoolName)
    {
        this.enemy = enemy;
        this.ESM = ESM;
        this.enemySoData = enemySoData;
        this.data = data;
        this.animBoolName = animBoolName;
        core = enemy.core;
    }
    public virtual void Enter()
    {

        startTime = Time.time;
        enemy.anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
        isExitingState = false;
    }
    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
        isExitingState = true;
    }
    public virtual void LogicUpdate()
    {
        DoChecks();
    }
    public virtual void PhysicsUpdate()
    {
       // DoChecks();
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
