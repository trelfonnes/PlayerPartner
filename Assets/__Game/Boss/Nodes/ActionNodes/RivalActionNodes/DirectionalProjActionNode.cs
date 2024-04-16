using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjActionNode : ActionNode
{
    private BossProjectile Projectile { get => projectile ?? componentLocator.GetCoreComponent(ref projectile); }
    private BossProjectile projectile;
    private BossCollisionDetection Collisions { get => collisions ?? componentLocator.GetCoreComponent(ref collisions); }
    private BossCollisionDetection collisions;
    private BossMelee Melee { get => melee ?? componentLocator.GetCoreComponent(ref melee); }
    private BossMelee melee;
    public DirectionalProjActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {
    }

    public override NodeState Execute()
    {
        if (Melee.currentState == BossMeleeState.active)
        {
            return NodeState.failure;
        }
        else
        {

            SetAnimation();
            ShootDirectionalProjectile();

            return NodeState.success;
        }
    }
    void ShootDirectionalProjectile()
    {
        Vector2 shootDir = new Vector2(Collisions.partnerTransform.position.x, Collisions.partnerTransform.position.y);
        Projectile.ShootProjectile(blackboard.projectileType, shootDir);

    }
    public override void SetAnimation()
    {
        base.SetAnimation();
    }
}
