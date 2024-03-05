using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoreComponent : MonoBehaviour, ILogicUpdate
{
    protected BossComponentLocator componentLocator;
    protected SpriteRenderer SR;
    // add reference to all the components the boss will potentiall have through the bossComponentLocator declare then store with getter and setter

    protected BossParticleFX ParticleFX { get => particleFX ?? componentLocator.GetCoreComponent(ref particleFX); }
    protected BossMovement Movement { get => movement ?? componentLocator.GetCoreComponent(ref movement); }
    protected BossCollisionDetection CollisionDetection { get => collisionDetection ?? componentLocator.GetCoreComponent(ref collisionDetection); } 

    private BossMovement movement;
    private BossCollisionDetection collisionDetection;
    private BossParticleFX particleFX;
    // private BossParticles bossParticles
   protected BossAI bossAI;
    protected virtual void Awake()
    {
        // initialize all components for the boss
        bossAI = GetComponentInParent<BossAI>();
        componentLocator = transform.parent.GetComponent<BossComponentLocator>();
        SR = GetComponentInParent<SpriteRenderer>();
        if (componentLocator == null)
        {
            Debug.LogError("There is no component locator on the boss parent");
        }
        componentLocator.AddComponent(this);
    }

    private void Update()
    {
        LogicUpdate();
    }
    public virtual void LogicUpdate() { }

}
