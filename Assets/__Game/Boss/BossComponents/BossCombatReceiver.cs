using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombatReceiver : BossCoreComponent, IDamageable
{
    [SerializeField] DefensiveType defensiveType;
    private IAttackTypeDamageCalculation defensiveStrategy;
    private BossCoreComp<BossStatsComponent> bossStats;
    private void Start()
    {
        bossStats = new BossCoreComp<BossStatsComponent>(componentLocator);
        SetDefensiveStrategy(defensiveType);
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

    }
}
