using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriReceiver : CoreComponent, IKnockBackable, IDamageable, IPoiseDamageable, IReviveAndRestore
{
  //  [SerializeField] GameObject damageParticles; //particles or vfx for when taking damage
  //  [SerializeField] GameObject stunnedParticles;

    public float maxKnockBackTime = .2f;
    float KnockBackStartTime;
    bool isKnockBackActive;

    IAttackTypeDamageCalculation defensiveStrategy;

    private CoreComp<Movement> movement;
    private CoreComp<CollisionSenses> collisionSenses;
    CoreComp<Health> health;
    CoreComp<Particles> particles;
    CoreComp<Poise> poise;
    CoreComp<Stats> stats;

  
    public void Damage(float amount, AttackType attackType)
    {
        float amountFloat = amount;

        float calculatedDamage = defensiveStrategy.CalculateDamageModifier(amountFloat, attackType);

        float calculatedDamageFloat = (float)calculatedDamage;
        AudioManager.Instance.PlayAudioClip("Hurt");

        health.Comp?.DecreaseHealth(calculatedDamageFloat);
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
    public void ReviveAndRestore()
    {
        stats.Comp?.ReviveAndRestore();
    }

    protected override void Awake()
    {
        base.Awake();
        poise = new CoreComp<Poise>(core);
        health = new CoreComp<Health>(core);
        particles = new CoreComp<Particles>(core);
        movement = new CoreComp<Movement>(core);
        stats = new CoreComp<Stats>(core);
        collisionSenses = new CoreComp<CollisionSenses>(core);
        SetDefensiveStrategy(health.Comp.defensiveType);
    }

    void SetDefensiveStrategy(DefensiveType defensiveType)
    {
      defensiveStrategy = DefensiveTypeStrategyFactory.CreateStrategy(defensiveType);

    }
}
