using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefeatedConditionNode : ConditionNode
{
 

    public BossDefeatedConditionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }
    public override NodeState Execute()
    {
        if (blackboard.isDefeated)
        {
            return NodeState.success;
        }
        else
        {
            return NodeState.failure;
        }
    }
}
