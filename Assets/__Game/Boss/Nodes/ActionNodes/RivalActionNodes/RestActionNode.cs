using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestActionNode : ActionNode
{
    Timer timer;
    float restTime;
    public RestActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {
        restTime = blackboard.restTime;
        StartRestTimer();
    }

    public override NodeState Execute()
    {
        

        timer.Update(Time.deltaTime);
        if (timer.IsFinished())
        {
            timer.Reset();
            return NodeState.success;
        }
        if (!timer.IsFinished())
        {
            blackboard.isFatigued = false;
        //    blackboard.canMove = true;

            return NodeState.failure;
        }
        else
        {
         //   blackboard.canMove = true;

            blackboard.isFatigued = false;
            return NodeState.failure;
        }
    }
    void StartRestTimer()
    {
        timer = new Timer(restTime);
    }
    public override void SetAnimation() //play the stunned character animation.
    {
        base.SetAnimation();
    }
}
