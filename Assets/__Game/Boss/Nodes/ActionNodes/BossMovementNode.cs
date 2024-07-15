using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovementNode : ActionNode
{
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    private BossCombatReceiver CombatReceiver { get => combatReceiver ?? componentLocator.GetCoreComponent(ref combatReceiver); }
    private BossCombatReceiver combatReceiver;
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    public BossMovementNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {

    }
    public override NodeState Execute()
    {
        Debug.Log(blackboard.moveSpeed);
            Movement.MoveTowards(blackboard.moveDirection, blackboard.moveSpeed);
            CombatReceiver.TurnCombatColliderOn(false);
            SetAnimation();
        return NodeState.success;
    }
    public override void SetAnimation()
    {
        base.SetAnimation();
    }
}
