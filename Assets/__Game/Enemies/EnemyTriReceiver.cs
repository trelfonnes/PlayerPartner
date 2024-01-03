using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriReceiver : CoreComponent, IKnockBackable, IDamageable, IPoiseDamageable
{

    private CoreComp<EnemyMovement> movement; //for knockback application
    private CoreComp<EnemyCollisionSenses> collisionSenses;
    private CoreComp<EnemyStats> stats;
    private CoreComp<Particles> particles;
 

    private IAttackTypeDamageCalculation defensiveStrategy;
    
    [SerializeField] float maxKnockBackTime = .2f;
    float KnockBackStartTime;
    bool isKnockBackActive;

    protected override void Awake()
    {
        base.Awake();
       
        stats = new CoreComp<EnemyStats>(core);
        particles = new CoreComp<Particles>(core);
        movement = new CoreComp<EnemyMovement>(core);
        collisionSenses = new CoreComp<EnemyCollisionSenses>(core);
        SetDefensiveStrategy(stats.Comp.defensiveType);
    }

    void SetDefensiveStrategy(DefensiveType defensiveType)
    {
        defensiveStrategy = DefensiveTypeStrategyFactory.CreateStrategy(defensiveType);

    }

    public void Damage(float amount, AttackType attackType)
    {
        float amountFloat = amount;

        float calculatedDamage = defensiveStrategy.CalculateDamageModifier(amountFloat, attackType);

        float calculatedDamageFloat = (float)calculatedDamage;

        stats.Comp?.DecreaseHealth(calculatedDamageFloat);
    }

    public void KnockBack(Vector2 angle, float strength, int directionX, int directionY)
    {
        movement.Comp?.TurnTowardsAttack(directionX, directionY);
        movement.Comp?.SetKnockBackVelocity(angle, strength, directionX, directionY);
        movement.Comp.CanSetVelocity = false;
        isKnockBackActive = true;
        KnockBackStartTime = Time.time;
    }
    public void DamagePoise(float amount) 
    {
        stats.Comp?.DamagePoise(amount);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckKnockBack();
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
}
