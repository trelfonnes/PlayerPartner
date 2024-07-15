using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalMeleeActionNode : ActionNode
{
    private BossMelee Melee { get => melee ?? componentLocator.GetCoreComponent(ref melee); }
    private BossMelee melee;
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    private BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    private BossMovement movement;
    private BossCombatReceiver CombatReceiver { get => combatReceiver ?? componentLocator.GetCoreComponent(ref combatReceiver); }
    private BossCombatReceiver combatReceiver;
    public DirectionalMeleeActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {
    }

    public override NodeState Execute()
    {
      
        AttackMelee();
        return NodeState.success;

    }
    void AttackMelee()
    {
        SetAnimation();
        SetAnimationFloat(Movement.CurrentDirection.x, movement.CurrentDirection.y);
        Melee.ExecuteDirectionalAttack(blackboard.anim, animBoolName, Movement.CurrentDirection.x, movement.CurrentDirection.y);
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
