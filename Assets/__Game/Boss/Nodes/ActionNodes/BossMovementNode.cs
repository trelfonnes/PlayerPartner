using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovementNode : ActionNode
{
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;

    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    public BossMovementNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }
    public override NodeState Execute()
    {
            Movement.MoveTowards(blackboard.moveDirection, blackboard.moveSpeed);
        Debug.Log(blackboard.moveDirection); 
        return NodeState.success;
    }
}
