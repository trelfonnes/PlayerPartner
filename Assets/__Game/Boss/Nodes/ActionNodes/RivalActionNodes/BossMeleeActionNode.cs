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
    private BossCombatReceiver CombatReceiver { get => combatReceiver ?? componentLocator.GetCoreComponent(ref combatReceiver); }
    private BossCombatReceiver combatReceiver;

    public BossMeleeActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {

    }

    public override NodeState Execute()
    {
        Movement.StopMovement();
            AttackMelee();
        CombatReceiver.TurnCombatColliderOn(true);

        return NodeState.success;
             
    }
    void AttackMelee()
    {
        SetAnimation();
        Melee.ExecuteAttack(blackboard.anim, animBoolName);
    }
    public override void SetAnimation()
    {
        base.SetAnimation();
    }
}
