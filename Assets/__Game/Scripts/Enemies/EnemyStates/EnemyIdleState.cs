using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBasicState
{
    float idleTime;
    bool isIdleTimeOver;
    bool changeDirAfterIdle;


    public EnemyIdleState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, string animBoolName) : base(enemy, ESM, enemySoData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        
        isIdleTimeOver = false;
        SetIdleTime();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
        
        if(isPlayerPartnerDetected)
        {
            Debug.Log("ENEMY detected Player/Partner In idle State");
            ESM.ChangeState(enemy.PlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {

            ESM.ChangeState(enemy.PatrolState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    void SetIdleTime()
    {
        idleTime = Random.Range(enemySoData.minIdleTime, enemySoData.maxIdleTime);
    }
}
