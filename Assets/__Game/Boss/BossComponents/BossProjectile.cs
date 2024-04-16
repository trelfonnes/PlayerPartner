using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : BossCoreComponent
{
    BossProjectileState currentState;



    public void ShootProjectile(ProjectileType projectileType, Vector2 direction) //called from node
    {
        // send the type from the blackboard and this transform position to projectile handler
        currentState = BossProjectileState.active;
        ProjectileEventSystem.Instance.RaiseSetProjectileTypeEvent(projectileType);
        ProjectileEventSystem.Instance.RaiseBossDirectionSetEvent(transform.position, direction);
    }
    public void AnimationCoolDown()
    {
        currentState = BossProjectileState.coolDown;
    }
    public void AnimationFinished()
    {
        currentState = BossProjectileState.idle;
    }
   
}
public enum BossProjectileState
{
    idle,
    active,
    coolDown
}
