using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjActionNode : ActionNode
{
    bool projectileShot = false;

    private BossProjectile Projectile { get => projectile ?? componentLocator.GetCoreComponent(ref projectile);}
    private BossProjectile projectile;
    public BossProjActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }
    public override NodeState Execute()
    {
       
            ShootProjectile();
            return NodeState.success;
        
    }
    void ShootProjectile()
    {
        Projectile.ShootProjectile(blackboard.projectileType, blackboard.projectileDirection);
        projectileShot = true;
    }
}
