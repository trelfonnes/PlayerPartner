using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStunConditionNode : ConditionNode
{
    public BossStunConditionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }
    public override NodeState Execute()
    {
        //check the condition of stun component being active state

        return NodeState.failure;

    }
}
