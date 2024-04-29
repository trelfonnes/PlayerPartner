using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatiguedConditionNode : ConditionNode
{
    private BossStatsComponent Stats { get => stats ?? componentLocator.GetCoreComponent(ref stats); }
    private BossStatsComponent stats;

    public FatiguedConditionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
       
    }

    public override NodeState Execute()
    {
       
        if (Stats.IsFatigued())
        {
            return NodeState.success;
        }
      
        else
        {

            return NodeState.failure;
        }
    }
}
