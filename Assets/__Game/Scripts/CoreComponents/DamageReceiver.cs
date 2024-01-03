using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : CoreComponent, IDamageable
{
    [SerializeField] GameObject damageParticles; //particles or vfx for when taking damage
    IAttackTypeDamageCalculation defensiveStrategy;

    CoreComp<Stats> stats;
    CoreComp<Health> health;
    CoreComp<Particles> particles;
    // CoreComp<Movement> movement;
    //CoreComp<CollisionSenses> collisionSenses;
    public void Damage(float amount, AttackType attackType)
    {
        float amountFloat = amount;

        float calculatedDamage = defensiveStrategy.CalculateDamageModifier(amountFloat, attackType);

        float calculatedDamageFloat = (float)calculatedDamage;

        health.Comp?.DecreaseHealth(calculatedDamageFloat);
    }



    protected override void Awake()
    {
        base.Awake();
        health = new CoreComp<Health>(core);
        particles = new CoreComp<Particles>(core);
        //not needed but here to grab when needed elsewhere like knockback and possibly in ones giving me errors
        //movement = new CoreComp<Movement>(core);
        //collisionSenses = new CoreComp<CollisionSenses>(core);
        SetDefensiveStrategy(health.Comp.defensiveType);
    }

    void SetDefensiveStrategy(DefensiveType defensiveType)
    {
        defensiveStrategy = DefensiveTypeStrategyFactory.CreateStrategy(defensiveType);

    }
}
