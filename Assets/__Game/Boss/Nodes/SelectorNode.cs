using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BehaviorNode
{

    private readonly List<BehaviorNode> nodeChildren;
    BossBlackboard blackboard;
    BossComponentLocator compLocator;

    public SelectorNode(BossComponentLocator locator, BossBlackboard blackboard, params BehaviorNode[] childNodes)
    {
        this.compLocator = locator;
        this.blackboard = blackboard;
        this.nodeChildren = new List<BehaviorNode>(childNodes);
    }

    public override NodeState Execute()
    {
       foreach (var childNode in nodeChildren)
        {
            NodeState result = childNode.Execute();
            if(result == NodeState.success)
            {
                return NodeState.success;
            }
        }
        return NodeState.failure;
    }
}
