using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : BehaviorNode
{
    private readonly List<BehaviorNode> nodeChildren;
    BossBlackboard blackboard;
    BossComponentLocator compLocator;
    public SequenceNode(BossComponentLocator locator, BossBlackboard blackboard, params BehaviorNode[] childNodes)
    {
        this.nodeChildren = new List<BehaviorNode>(childNodes);
    }

    public override NodeState Execute()
    {
        foreach(var childNode in nodeChildren)
        {
            NodeState result = childNode.Execute();
            if(result == NodeState.failure)
            {

                return NodeState.failure;

            }
        }

        return NodeState.success;
    }
}
