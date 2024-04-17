using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConditionNode : ConditionNode
{
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    private BossMelee Melee { get => melee ?? componentLocator.GetCoreComponent(ref melee); }
    private BossMelee melee;
    public MoveConditionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
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
