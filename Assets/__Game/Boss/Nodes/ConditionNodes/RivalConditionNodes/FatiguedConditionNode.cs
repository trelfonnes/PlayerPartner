using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatiguedConditionNode : ConditionNode
{
    public FatiguedConditionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
       
    }

    public override NodeState Execute()
    {
        if (blackboard.isFatigued)
        {
            return NodeState.success;
        }
        else
        {
            return NodeState.failure;
        }
    }
}
