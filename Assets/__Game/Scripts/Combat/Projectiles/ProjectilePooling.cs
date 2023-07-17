using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooling : MonoBehaviour
{
    //needs to listen to projectile eventhandler to unpool
    //projectile itself needs to listen to projectileEventHandler to add direction and velocity

    private void OnEnable()
    {
        ProjectileEventSystem.Instance.OnSetProjectileType += UnpoolProjectile;
    }
    private void OnDisable()
    {
        ProjectileEventSystem.Instance.OnSetProjectileType -= UnpoolProjectile;
    }

    public void UnpoolProjectile(ProjectileType type)
    {
        Debug.Log(type);
        if(type == ProjectileType.BasicProjectile)
        {
            //unpool a basicProjectile
        }
        if(type == ProjectileType.ChargeProjectile)
        {

        }
        if(type == ProjectileType.SpreadProjectile)
        {

        }
        if(type == ProjectileType.PlayerProjectile)
        {

        }
    }




}
