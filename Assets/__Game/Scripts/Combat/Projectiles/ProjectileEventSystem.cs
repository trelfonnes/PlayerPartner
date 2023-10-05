using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileEventSystem : MonoBehaviour
{
    public static ProjectileEventSystem Instance;

    public event Action<PartnerProjectile, Vector2> OnPartnerDirectionSet;
    public event Action<Projectile, Vector2> OnPlayerDirectionSet;
    // create an event for unpooling as well??
    public event Action<ProjectileType> OnSetProjectileType;
    public event Action<bool> OnPartnerShotIsCharged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void RaisePartnerDirectionSetEvent(PartnerProjectile projectileComponent, Vector2 direction)
    {
        OnPartnerDirectionSet?.Invoke(projectileComponent, direction); //listened to by Partner specific projectiles
    }    
    public void RaisePartnerShotIsCharged(bool charged)
    {
        OnPartnerShotIsCharged?.Invoke(charged);
    }
    public void RaisePlayerDirectionSetEvent(Projectile projectileComponent, Vector2 direction)
    {
        OnPlayerDirectionSet?.Invoke(projectileComponent, direction); //listened to by player specific projectiles
    }
    public void RaiseSetProjectileTypeEvent(ProjectileType type)
    {
        OnSetProjectileType?.Invoke(type);
    }
}
