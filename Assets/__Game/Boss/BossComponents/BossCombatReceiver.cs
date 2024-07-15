using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombatReceiver : BossCoreComponent, IDamageable, IKnockBackable
{
    [SerializeField] DefensiveType defensiveType;
    private IAttackTypeDamageCalculation defensiveStrategy;
    private BossCoreComp<BossStatsComponent> bossStats;
    private BossCoreComp<BossMovement> movement;
    BoxCollider2D combatCollider;
    [SerializeField] bool enableCombatCollider;

    [SerializeField] float maxKnockBackTime = .2f;
    float KnockBackStartTime;
    bool isKnockBackActive;
    private void Start()
    {
        bossStats = new BossCoreComp<BossStatsComponent>(componentLocator);
        movement = new BossCoreComp<BossMovement>(componentLocator);
        SetDefensiveStrategy(defensiveType);
        combatCollider = GetComponent<BoxCollider2D>();
        combatCollider.enabled = enableCombatCollider;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    void SetDefensiveStrategy(DefensiveType defensiveType)
    {
        defensiveStrategy = DefensiveTypeStrategyFactory.CreateStrategy(defensiveType);

    }
    public void TurnCombatColliderOn(bool onOff)
    {
        combatCollider.enabled = onOff;
    }
    
    public void Damage(float amount, AttackType attackType)
    {
        float amountFloat = amount;
        float calculatedDamage = defensiveStrategy.CalculateDamageModifier(amountFloat, attackType);
        float calculatedDamageFloat = (float)calculatedDamage;
        AudioManager.Instance.PlayAudioClip("Damaged");
        bossStats.Comp.DecreaseHealth(calculatedDamageFloat);
        //  Debug.Log("The boss movement reference is present");
         //   movement.Comp.SetKnockBackVelocity(Vector2.down, 5f, (int)-movement.Comp.CurrentDirection.x, (int)-movement.Comp.CurrentDirection.y);
        //   Debug.Log("NO movement reference on the boss combat receiver");
        
    }

    public void KnockBack(Vector2 angle, float strength, int directionX, int directionY)
    {
        movement.Comp?.SetKnockBackVelocity(angle, strength, directionX, directionY);
        isKnockBackActive = true;
        KnockBackStartTime = Time.time;
    }

   
}
