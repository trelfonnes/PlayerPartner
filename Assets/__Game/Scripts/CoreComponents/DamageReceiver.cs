using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : CoreComponent, IDamageable
{
    [SerializeField] GameObject damageParticles; //particles or vfx for when taking damage

    CoreComp<Stats> stats;
    CoreComp<Health> health;
    CoreComp<Particles> particles;
   // CoreComp<Movement> movement;
    //CoreComp<CollisionSenses> collisionSenses;
    public void Damage(float amount)
    {
        health.Comp?.DecreaseHealth(amount);  // needs to send amount to the Health component
        // need reference to health component
        particles.Comp?.StartParticlesWithRandomRotation(damageParticles); //need to start particles with reference to the particle manager
    }

    protected override void Awake()
    {
        base.Awake();
        health = new CoreComp<Health>(core);
        particles = new CoreComp<Particles>(core);
        //not needed but here to grab when needed elsewhere like knockback and possibly in ones giving me errors
        //movement = new CoreComp<Movement>(core);
        //collisionSenses = new CoreComp<CollisionSenses>(core);
    }
}
