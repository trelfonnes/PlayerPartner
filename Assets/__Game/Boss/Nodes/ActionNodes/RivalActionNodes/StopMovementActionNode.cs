using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovementActionNode : ActionNode
{
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    public StopMovementActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {
    }

    public override NodeState Execute()
    {
        Movement.StopMovement();
        Debug.Log("MOVEMENT IS  BEING CALLED TO STOP");
        return NodeState.success;
    
    }

    public override void SetAnimation()
    {
        base.SetAnimation();
    }

    public override void SetAnimationFloat(float moveX, float moveY)
    {
        base.SetAnimationFloat(moveX, moveY);
    }
}
