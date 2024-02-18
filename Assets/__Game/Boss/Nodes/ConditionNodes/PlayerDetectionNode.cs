using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionNode : ConditionNode
{
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
        private BossCollisionDetection collisions;

    public PlayerDetectionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }
    public override NodeState Execute()
    {
        if (Collisions.IsPlayerInFieldOfView)//playeris detected form boss collision detection){
        {
            return NodeState.success;
        }
        else
            return NodeState.success;
    }
}
