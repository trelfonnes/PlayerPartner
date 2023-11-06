using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriReceiver : CoreComponent, IKnockBackable, IDamageable, IPoiseDamageable
{
  //  [SerializeField] GameObject damageParticles; //particles or vfx for when taking damage
  //  [SerializeField] GameObject stunnedParticles;

    [SerializeField] float maxKnockBackTime = .2f;
    float KnockBackStartTime;
    bool isKnockBackActive;

    IAttackTypeDamageCalculation defensiveStrategy;

    private CoreComp<Movement> movement;
    private CoreComp<CollisionSenses> collisionSenses;
    CoreComp<Health> health;
    CoreComp<Particles> particles;
    CoreComp<Poise> poise;

  
    public void Damage(float amount, AttackType attackType)
    {// casting because I messed up and used int in concrete strategies instead of float.
        //TODO: make them use float instead of int
        int amountInt = (int)amount;
       
        int calculatedDamage = defensiveStrategy.CalculateDamageModifier(amountInt, attackType);

       float calculatedDamageFloat = (float)calculatedDamage;

        health.Comp?.DecreaseHealth(calculatedDamageFloat);  // needs to send amount to the Health component
       // particles.Comp?.StartParticlesWithRandomRotation(damageParticles); //need to start particles with reference to the particle manager
    }
    public void DamagePoise(float amount)
    {
        
            poise.Comp?.DamagePoise(amount);
            // particles.StartParticlesWithRandomRotation(stunnedParticles); //TODO: might not be best way to set stun particles if any
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckKnockBack();
    }

    public void KnockBack(Vector2 angle, float strength, int directionX, int directionY)
    {
        Debug.Log("KNockback applied on " + gameObject.name);
        movement.Comp?.SetKnockBackVelocity(angle, strength, directionX, directionY);
        movement.Comp.CanSetVelocity = false;
        isKnockBackActive = true;
        KnockBackStartTime = Time.time;

    }

    void CheckKnockBack()
    {
        if (isKnockBackActive && Time.time >= KnockBackStartTime + maxKnockBackTime) // extra condition for a side scroller to include grounded and no y velocity being applied
        {
            movement.Comp?.SetVelocityZero();
            isKnockBackActive = false;
            movement.Comp.CanSetVelocity = true;
        }



    }

    protected override void Awake()
    {
        base.Awake();
        poise = new CoreComp<Poise>(core);
        health = new CoreComp<Health>(core);
        particles = new CoreComp<Particles>(core);
        movement = new CoreComp<Movement>(core);
        collisionSenses = new CoreComp<CollisionSenses>(core);
        SetDefensiveStrategy(health.Comp.defensiveType);
    }

    void SetDefensiveStrategy(DefensiveType defensiveType)
    {
      defensiveStrategy = DefensiveTypeStrategyFactory.CreateStrategy(defensiveType);

    }
}
