using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombatReceiver : BossCoreComponent, IDamageable
{
    [SerializeField] DefensiveType defensiveType;
    private IAttackTypeDamageCalculation defensiveStrategy;
    private BossCoreComp<BossStatsComponent> bossStats;
    BoxCollider2D combatCollider;
    private void Start()
    {
        bossStats = new BossCoreComp<BossStatsComponent>(componentLocator);
        SetDefensiveStrategy(defensiveType);
        combatCollider = GetComponent<BoxCollider2D>();
        combatCollider.enabled = false;
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
        bossStats.Comp.DecreaseHealth(calculatedDamageFloat);

    }
}
