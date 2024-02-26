using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionNode : BehaviorNode
{
    protected BossBlackboard blackboard;
    protected BossComponentLocator componentLocator;
    protected string animBoolName;
    public ActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
        this.animBoolName = animBoolName;

    }
   public virtual void SetAnimation()
    {
        blackboard.anim.SetTrigger(animBoolName);
        
    }

}
