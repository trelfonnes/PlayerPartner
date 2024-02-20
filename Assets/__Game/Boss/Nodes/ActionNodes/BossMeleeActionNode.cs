using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeActionNode : ActionNode
{
    bool meleeExecuted = false;
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
        if (!meleeExecuted)
        {
            Movement.HoldMovementForMelee();
            AttackMelee();
            return NodeState.success;

        }
        else
        {
            meleeExecuted = false;
            return NodeState.failure;
        }
    }
    void AttackMelee()
    {
        Debug.Log("Melee");
        meleeExecuted = true;
        Movement.ContinueMovement(blackboard.meleeTime);
    }
}
