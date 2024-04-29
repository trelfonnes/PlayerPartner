using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConditionNode : ConditionNode
{
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    private BossMelee Melee { get => melee ?? componentLocator.GetCoreComponent(ref melee); }
    private BossMelee melee;
    private BossStatsComponent Stats { get => stats ?? componentLocator.GetCoreComponent(ref stats); }
    private BossStatsComponent stats;
    public MoveConditionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }

    public override NodeState Execute()
    {
     
        if (Movement.CanMove() && !Stats.IsFatigued())
        {
            return NodeState.success;
        }
        else
        {
            return NodeState.failure;
        }
    }
}
