using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveTypeStrategyFactory 
{
    public static IAttackTypeDamageCalculation CreateStrategy(DefensiveType defensiveType)
    {
        switch (defensiveType)
        {
            case DefensiveType.Fire:
                return new FireTypeStrategy();
            case DefensiveType.Water:
                return new WaterTypeStrategy();
            case DefensiveType.air:
                return new AirTypeStrategy();
            case DefensiveType.Electric:
                return new ElectricTypeStrategy();
            case DefensiveType.Ground:
                return new GroundTypeStrategy();
            case DefensiveType.Poison:
                return new PoisonTypeStrategy();
            case DefensiveType.Normal:
                return new NormalTypeStrategy();


            default:
                return new NormalTypeStrategy(); //TODO: make default normalType strategy
        }
    }


}
