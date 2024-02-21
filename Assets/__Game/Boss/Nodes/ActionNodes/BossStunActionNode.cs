using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStunActionNode : ActionNode
{
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    public BossStunActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;

    }

    public override NodeState Execute()
    {
        //Play the stunned animation and access the movement component to stop movement;
        Movement.StopMovement();
        // this will be exited via the decorator reading the stun component

        return NodeState.success;
    }
}
