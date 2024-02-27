using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjActionNode : ActionNode
{
    bool projectileShot = false;
    private BossMelee Melee { get => melee ?? componentLocator.GetCoreComponent(ref melee); }
    private BossMelee melee;
    private BossStunned Stunned { get => stunned ?? componentLocator.GetCoreComponent(ref stunned); }
    private BossStunned stunned;
    private BossProjectile Projectile { get => projectile ?? componentLocator.GetCoreComponent(ref projectile);}
    private BossProjectile projectile;
    public BossProjActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator, string animBoolName) : base(blackboard, componentLocator, animBoolName)
    {

    }
    public override NodeState Execute()
    {
        if (Stunned.IsStunActive())
        {
            return NodeState.failure;
        }
        if (Melee.currentState == BossMeleeState.active)
        {
            return NodeState.failure;
        }
        
        else
        {
            ShootProjectile();
            return NodeState.success;
        }
    }
    void ShootProjectile()
    {
        Projectile.ShootProjectile(blackboard.projectileType, blackboard.projectileDirection);
        projectileShot = true;
    }
}
