using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeActionNode : ActionNode
{
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    
    public BossChargeActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {
    }

    public override NodeState Execute()
    {
        blackboard.stamina -= Time.deltaTime;
        if (blackboard.stamina <= 0 && !blackboard.isFatigued)
        {
            blackboard.isFatigued = true;
            return NodeState.success;
        }
        Movement.ChargePlayer(blackboard.moveSpeed, Collisions.partnerTransform, blackboard.chargeBuffer);
        SetAnimation();
        return NodeState.success;
    }

    public override void SetAnimation()
    {
        base.SetAnimation();
    }
}
