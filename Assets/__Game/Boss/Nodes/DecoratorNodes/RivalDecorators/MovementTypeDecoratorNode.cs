using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTypeDecoratorNode : DecoratorNode
{
    private readonly List<BehaviorNode> nodeChildren;
    public MovementTypeDecoratorNode(BossBlackboard blackboard, BossComponentLocator componentLocator, params BehaviorNode[] childNodes)
    {
        this.nodeChildren = new List<BehaviorNode>(childNodes);
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }

    public override NodeState Execute()
    {

        if (blackboard.isLowHealth)
        {//[1] equals low health moveAction node
            NodeState result = nodeChildren[1].Execute();
            return result;
        }
        else
        {
            NodeState result = nodeChildren[0].Execute();
            return result;
        }


    }
}
