using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDistanceActionNode : ActionNode
{
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    public BossDistanceActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {

    }

    public override NodeState Execute()
    {
        blackboard.stamina -= Time.deltaTime;
        if(blackboard.stamina <= 0 && !blackboard.isFatigued)
        {
            blackboard.isFatigued = true;
            return NodeState.success;
        }
        Movement.KeepDistance(blackboard.moveSpeed, Collisions.partnerTransform, blackboard.distancingLength, Collisions.WallCheck);
        Collisions.UpdateFOVDirection(Movement.CurrentDirection);
        SetAnimation();
        SetAnimationFloat(Movement.CurrentDirection.x, Movement.CurrentDirection.y);
        return NodeState.success;
        
    }
    public override void SetAnimationFloat(float moveX, float moveY)
    {
        base.SetAnimationFloat(moveX, moveY);
    }
    public override void SetAnimation()
    {
        base.SetAnimation();
    }
}
