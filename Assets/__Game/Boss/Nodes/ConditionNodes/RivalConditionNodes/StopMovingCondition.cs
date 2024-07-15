using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovingCondition : ConditionNode
{
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    public StopMovingCondition(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }

    public override NodeState Execute()
    {
        if (Movement.CanMove())
        {
            return NodeState.success;
        }
        else
        {
            return NodeState.failure;
        }
    
                
    }
}
