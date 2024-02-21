using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeActionNode : ActionNode
{
    private BossMelee Melee { get => melee ?? componentLocator.GetCoreComponent(ref melee); }
    private BossMelee melee;
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;

    public BossMeleeActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.componentLocator = componentLocator;
        this.blackboard = blackboard;
    }

    public override NodeState Execute()
    {
            AttackMelee();
            return NodeState.success;
             
    }
    void AttackMelee()
    {
        Melee.ExecuteAttack();
    }
}
