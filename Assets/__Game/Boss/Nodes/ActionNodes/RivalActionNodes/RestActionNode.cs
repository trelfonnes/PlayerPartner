using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestActionNode : ActionNode
{
    Timer timer;
    float restTime;

    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    private BossStatsComponent Stats { get => stats ?? componentLocator.GetCoreComponent(ref stats); }
    private BossStatsComponent stats;

    public RestActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {
       
       
    }

    public override NodeState Execute()
    {
        Movement.MoveOnOff(false);
        Stats.RestoreStamina();
        if (Stats.IsFatigued())
        {
            SetAnimation();
            return NodeState.success;
        }
        else
        {
            return NodeState.failure;
        }
              
    }
    
    public override void SetAnimation() //play the stunned character animation.
    {
        base.SetAnimation();
    }
}
