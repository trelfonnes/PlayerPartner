using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjActionNode : ActionNode
{
    bool projectileShot = false;
    public BossProjActionNode(BossBlackboard blackboard, BossComponentLocator componentLocator)
    {
        this.blackboard = blackboard;
        this.componentLocator = componentLocator;
    }
    public override NodeState Execute()
    {
        if (!projectileShot)
        {
            ShootProjectile();
            return NodeState.success;
        }
        else
        {
            projectileShot = false;
            return NodeState.failure;
        }
    }
    void ShootProjectile()
    {
        Debug.Log("FIRE!");
        projectileShot = true;
    }
}
