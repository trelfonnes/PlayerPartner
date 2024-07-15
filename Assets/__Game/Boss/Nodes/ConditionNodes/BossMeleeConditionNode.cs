using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeConditionNode : ConditionNode
{
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;

    public BossMeleeConditionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }
    public override NodeState Execute()
    {
        if (Collisions.IsPlayerInFieldOfView)
        {
            return NodeState.success;
        }
        else
        {
            return NodeState.failure;
        }
    }
}
